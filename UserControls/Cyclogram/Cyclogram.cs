using Bachelor_Project_Hydrogen_Compression_WinForms.Miscellaneous;
using Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class Cyclogram : UserControl
    {
        private bool _active;
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                timer_main.Enabled = _active;
            }
        }

        public Action<string, string> OnComponentStatusChange;

        public enum CyclogramPlayMode { Single, Loop };

        public CyclogramPlayMode PlayMode;

        public int CurrentTimeStamp { get; set; }

        public List<CyclogramComponentElement> Components = new List<CyclogramComponentElement>();
        public List<CyclogramStatusElement> Statuses = new List<CyclogramStatusElement>();
        public List<CyclogramStepElement> Steps = new List<CyclogramStepElement>();

        public float TitleWidthRatio = 0.2f;
        public float CyclogramRulerHeightRatio = 0.1f;

        public int VerticalScrollerPos = 0;
        public int HorizontalScrollerPos = 0;

        public int ScrollerWidth = 20;

        public int MaxSimultaneousRecords = 20;
        public int MaxSimultaneousSteps = 8;

        private bool _shiftKeyPressed;


        public int GetTotalLengthInMilliseconds 
        { 
            get 
            {
                int length = 0;

                foreach (CyclogramStepElement step in Steps)
                {
                    length += step.LengthMilliseconds;
                }

                return length;
            } 
        }

        #region Rectangles

        public Rectangle GetCyclogramTitleRect => new Rectangle(
            0,
            0,
            (int)(this.Width * TitleWidthRatio),
            (int)(this.Height * CyclogramRulerHeightRatio)
        );

        public Rectangle GetStepsRect => new Rectangle(
            (int)(this.Width * TitleWidthRatio),
            0,
            (int)(this.Width * (1 - TitleWidthRatio)) - ScrollerWidth,
            (int)(this.Height * CyclogramRulerHeightRatio)
        );

        public Rectangle GetTitlesRect => new Rectangle(
            0,
            (int)(this.Height * CyclogramRulerHeightRatio),
            (int)(this.Width * TitleWidthRatio),
            (int)(this.Height - (int)(this.Height * CyclogramRulerHeightRatio)) - ScrollerWidth
        );

        public Rectangle GetSequencesRect => new Rectangle(
            (int)(this.Width * TitleWidthRatio),
            (int)(this.Height * CyclogramRulerHeightRatio),
            (int)(this.Width * (1 - TitleWidthRatio)) - ScrollerWidth,
            (int)(this.Height - (int)(this.Height * CyclogramRulerHeightRatio)) - ScrollerWidth
        );

        public Rectangle GetVerticalScrollerRect => new Rectangle(
            this.Width - ScrollerWidth,
            0,
            ScrollerWidth,
            this.Height            
        );

        public Rectangle GetHorizontalScrollerRect => new Rectangle(
            0,
            this.Height - ScrollerWidth,
            this.Width,
            ScrollerWidth
        );

        #endregion


        #region Colors

        public Color OutlineColor = Color.FromArgb(32, 30, 55);
        public Color CategoryFillColor = Color.FromArgb(42, 40, 65);
        public Color TitleFillColor = Color.FromArgb(52, 50, 75);
        public Color CyclogramTitleColor = Color.FromArgb(93, 91, 122);

        public Color BackgroundColor = Color.FromArgb(32, 30, 45);
        public Color SecondBackgroundColor = Color.FromArgb(23, 21, 32);

        public Color DefaultTextColor = Color.FromArgb(180, 160, 220);
        public Color BrightTextColor = Color.FromArgb(250, 250, 250);

        #endregion

        public Cyclogram()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.MouseWheel += MouseWheelScroll;
        }

        #region Governance

        public void Play(bool value)
        {
            this.Active = value;
            this.Refresh();
        }

        public void StepForward()
        {
            bool process = false;

            for (int i = 0; i < this.Steps.Count; i++)
            {
                int traveledLength = i > 0 ? Steps.Take(i).Sum((x) => x.LengthMilliseconds) : 0;

                if (process)
                {
                    this.CurrentTimeStamp = traveledLength;
                    this.CheckSteps();
                    break;
                }

                bool isActive = Mathf.Between(this.CurrentTimeStamp, traveledLength, 
                                                  traveledLength + this.Steps[i].LengthMilliseconds - 1);

                if(isActive)
                {
                    process = true;
                    continue;
                }
            }

            this.Refresh();
        }

        public void StepBackward()
        {
            for (int i = 0; i < this.Steps.Count; i++)
            {
                int traveledLength = i > 0 ? Steps.Take(i).Sum((x) => x.LengthMilliseconds) : 0;

                bool isActive = Mathf.Between(this.CurrentTimeStamp, traveledLength,
                                                  traveledLength + this.Steps[i].LengthMilliseconds - 1);

                if (isActive)
                {
                    traveledLength = i - 1 > 0 ? Steps.Take(i - 1).Sum((x) => x.LengthMilliseconds) : 0;

                    this.CurrentTimeStamp = traveledLength;
                    this.CheckSteps();
                    break;
                }
            }

            this.Refresh();
        }

        public void SetRightEnd()
        {
            int traveledLength = this.Steps.Count - 1 > 0 ? Steps.Take(this.Steps.Count - 1).Sum((x) => x.LengthMilliseconds) : 0;

            this.CurrentTimeStamp = traveledLength;
            this.CheckSteps();

            this.Refresh();
        }

        public void SetLeftEnd()
        {
            this.CurrentTimeStamp = 0;
            this.CheckSteps();

            this.Refresh();
        }

        #endregion

        private void IncrementTimeStamp()
        {
            if (this.CurrentTimeStamp >= this.GetTotalLengthInMilliseconds)
            {
                if (this.PlayMode == Cyclogram.CyclogramPlayMode.Single)
                {
                    return;
                }
                else if (this.PlayMode == Cyclogram.CyclogramPlayMode.Loop)
                {
                    this.CurrentTimeStamp = 0;

                    Console.WriteLine("Cyclogram reached the end. Starting a new cycle.");
                }
            }

            this.CurrentTimeStamp += timer_main.Interval;

            if (this.CurrentTimeStamp >= this.GetTotalLengthInMilliseconds &&
                this.PlayMode == Cyclogram.CyclogramPlayMode.Single)
            {
                Console.WriteLine("Cyclogram reached the end.");
            }

            CheckSteps();
        }

        private void CheckSteps()
        {
            for (int i = 0; i < this.Steps.Count; i++)
            {
                int traveledLength = 0;

                for (int j = 0; j < i; j++)
                {
                    traveledLength += this.Steps[j].LengthMilliseconds;
                }

                bool shouldBeActive = this.CurrentTimeStamp >= traveledLength
                    && this.CurrentTimeStamp < traveledLength + this.Steps[i].LengthMilliseconds;

                foreach (CyclogramSequenceElement sequence in this.Steps[i].Sequences)
                {
                    if (sequence.Active ^ shouldBeActive) // ^ stands for XOR - means (1 && 0 or 0 && 1)
                    {
                        sequence.Active = !sequence.Active;

                        Console.WriteLine($"Sequence [{sequence.SequenceID}] has been {(sequence.Active ? "activated" : "deactivated")}");

                        foreach (CyclogramStatusElement title in this.Statuses)
                        {
                            if (title.TitleID.Equals(sequence.TitleID))
                            {
                                Console.WriteLine($"Title [{title.Text}] has been {(sequence.Active ? "activated" : "deactivated")}");

                                if (sequence.Active)
                                {
                                    string[] words = title.TitleID.Split('_');
                                    if (words.Length == 2)
                                    {
                                        OnComponentStatusChange?.Invoke(words[0], words[1]);
                                    }
                                }

                                break;
                            }
                        }
                    }
                }

            }
        }

        private void Cyclogram_Paint(object sender, PaintEventArgs e)
        {
            if (Steps.Count == 0) return;

            Graphics g = e.Graphics;

            Rectangle MainTitleRect = GetCyclogramTitleRect;
            Rectangle StepsRect = GetStepsRect;
            Rectangle TitlesRect = GetTitlesRect;
            Rectangle SequencesRect = GetSequencesRect;

            //int oneCellHeight = (this.Height - StepsRect.Height) / (Components.Count + Statuses.Count);
            int oneCellHeight = (this.GetTitlesRect.Height) / (MaxSimultaneousRecords);

            // Drawing Background
            g.FillRectangle(new SolidBrush(BackgroundColor), new Rectangle(default(Point), this.Size));

            // Drawing Cyclogram Name
            {
                g.FillRectangle(new SolidBrush(CyclogramTitleColor), new RectangleF(MainTitleRect.Location, MainTitleRect.Size));
                g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(MainTitleRect.Location, MainTitleRect.Size));
                g.DrawString(" " + "Cyclogram Name", Font, new SolidBrush(BrightTextColor), MainTitleRect.Location);
            }

            // Drawing Steps

            int[] stepWidths = new int[Steps.Count];
            {
                Point stepsLocation = StepsRect.Location;

                int lineWidth = StepsRect.Width;
                int totalLengthMilliseconds = 0;

                for(int i = 0; i < MaxSimultaneousSteps && HorizontalScrollerPos + i < Steps.Count; i++)
                {
                    totalLengthMilliseconds += Steps[HorizontalScrollerPos + i].LengthMilliseconds;
                }

                for (int i = 0; i < MaxSimultaneousSteps && HorizontalScrollerPos + i < Steps.Count; i++)
                {
                    int operationsWidth = (int)(lineWidth * ((float)Steps[HorizontalScrollerPos + i].LengthMilliseconds / totalLengthMilliseconds));
                    totalLengthMilliseconds -= Steps[HorizontalScrollerPos + i].LengthMilliseconds;
                    lineWidth -= operationsWidth;

                    g.FillRectangle(new SolidBrush(CategoryFillColor), new RectangleF(stepsLocation, new Size(operationsWidth, StepsRect.Height)));
                    g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(stepsLocation, new Size(operationsWidth, StepsRect.Height)));
                    g.DrawString(" " + Steps[HorizontalScrollerPos + i].Name, Font, new SolidBrush(BrightTextColor), stepsLocation);
                    stepsLocation.X += operationsWidth;

                    stepWidths[HorizontalScrollerPos + i] = operationsWidth;
                }
            }

            // Drawing roads
            {
                int segments = 4 * Steps.Count;

                Point location = SequencesRect.Location;

                for (int i = 0; i < Components.Count + Statuses.Count; i++)
                {
                    int lineWidth = SequencesRect.Width;

                    for (int j = 0; j < segments; j++)
                    {
                        int segmentWidth = lineWidth / (segments - j);
                        lineWidth -= segmentWidth;

                        if (j % 2 == 0)
                            g.FillRectangle(new SolidBrush(SecondBackgroundColor), new Rectangle(location, new Size(segmentWidth, oneCellHeight)));

                        g.DrawRectangle(new Pen(OutlineColor), new Rectangle(location, new Size(segmentWidth, oneCellHeight)));
                        location.X += segmentWidth;
                    }
                    location.X = SequencesRect.Left;
                    location.Y += oneCellHeight;
                }
            }

            int countOfRecords = 0;
            int countOfSkips = VerticalScrollerPos;

            // Drawing Categories and Titles
            {
                Size oneCellSize = new Size(TitlesRect.Width, oneCellHeight);
                Point location = TitlesRect.Location;

                foreach (CyclogramComponentElement category in Components)
                {
                    if(countOfSkips == 0)
                    {
                        if (countOfRecords >= MaxSimultaneousRecords) break;

                        countOfRecords++;

                        g.FillRectangle(new SolidBrush(CategoryFillColor), new RectangleF(location, oneCellSize));
                        g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(location, oneCellSize));
                        g.DrawString(" " + category.Name, Font, new SolidBrush(BrightTextColor), location + new Size(0, oneCellHeight / 3));

                        int rectSide = oneCellSize.Width > oneCellSize.Height ? oneCellSize.Height / 5 : oneCellSize.Width / 5;

                        Point p = new Point(location.X + oneCellSize.Width - rectSide, location.Y);
                        g.DrawRectangle(new Pen(OutlineColor), new Rectangle(p, new Size(rectSide, oneCellSize.Height)));
                        g.FillRectangle(new SolidBrush(CyclogramTitleColor), new Rectangle(p, new Size(rectSide, oneCellSize.Height)));

                        location.Y += oneCellHeight;
                    }
                    else
                    {
                        countOfSkips--;
                    }

                    

                    foreach (CyclogramStatusElement title in category.Titles)
                    {
                        if(countOfSkips > 0)
                        {
                            countOfSkips--;
                            continue;
                        }

                        if (countOfRecords >= MaxSimultaneousRecords) break;

                        countOfRecords++;

                        g.FillRectangle(new SolidBrush(TitleFillColor), new RectangleF(location, oneCellSize));
                        g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(location, oneCellSize));
                        g.DrawString("   " + title.Text, Font, new SolidBrush(DefaultTextColor), location + new Size(0, oneCellSize.Height / 3));

                        // Drawing sequences

                        for (int k = 0; k < Steps.Count; k++)
                        {
                            int currentCellPos = 0;
                            
                            for (int k2 = 0; k2 < k; k2++)
                            {
                                currentCellPos += stepWidths[k2];
                            }

                            for (int i = 0; i < Steps[k].Sequences.Count; i++)
                            {
                                if (!Steps[k].Sequences[i].TitleID.Equals(title.TitleID)) continue;

                                LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                   location + new Size(SequencesRect.Left + currentCellPos, 0),
                                   location + new Size(SequencesRect.Left + currentCellPos, 0) + new Size(stepWidths[k], oneCellHeight),
                                   CyclogramTitleColor,
                                   DefaultTextColor);

                                g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(location + new Size(SequencesRect.Left + currentCellPos, 0), new Size(stepWidths[k], oneCellHeight)));
                                //g.DrawString(Steps[k].Sequences[i].SequenceID, this.Font, new SolidBrush(Color.FromArgb(255, 0, 0)), new Rectangle(location + new Size(stepWidths[k], 0), new Size(SequencesRect.Width / Steps.Count, oneCellHeight)));
                                g.FillRectangle(linGrBrush, new Rectangle(location + new Size(SequencesRect.Left + currentCellPos, 0), new Size(stepWidths[k], oneCellHeight)));

                                

                                if (Steps[k].Sequences[i].Active)
                                {
                                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0), 2), new Rectangle(location + new Size(SequencesRect.Left + currentCellPos, 0), new Size(stepWidths[k], oneCellHeight)));
                                }
                            }
                        }

                        location.Y += oneCellHeight;
                    }
                }
            }

            // Drawing Time Stamp line

            int beforeLengthMilliseconds = 0;
            int visibleLengthMilliseconds = 0;

            Point timeStampLocation;

            if (GetTotalLengthInMilliseconds != 0)
            {
                for (int i = 0; i < HorizontalScrollerPos; i++)
                {
                    beforeLengthMilliseconds += Steps[i].LengthMilliseconds;
                }

                for (int i = 0; i < MaxSimultaneousSteps && HorizontalScrollerPos + i < Steps.Count; i++)
                {
                    visibleLengthMilliseconds += Steps[HorizontalScrollerPos + i].LengthMilliseconds;
                }

                if (Mathf.Between(CurrentTimeStamp, beforeLengthMilliseconds, beforeLengthMilliseconds + visibleLengthMilliseconds))
                {
                    timeStampLocation = new Point(SequencesRect.Left + (int)(SequencesRect.Width * Mathf.NormalizedRelationBetween(CurrentTimeStamp, beforeLengthMilliseconds, beforeLengthMilliseconds + visibleLengthMilliseconds)), SequencesRect.Top);

                    //timeStampLocation = new Point(SequencesRect.Left + (int)(SequencesRect.Width * ((float)CurrentTimeStamp / GetTotalLengthInMilliseconds)), SequencesRect.Top);

                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0), 2), new Rectangle(timeStampLocation, new Size(1, SequencesRect.Height)));
                }
            }
            else
            {
                timeStampLocation = new Point(SequencesRect.Left, SequencesRect.Top);
                g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0), 2), new Rectangle(timeStampLocation, new Size(1, SequencesRect.Height)));
            }


            // Drawing Vertical Scroller
            g.DrawRectangle(new Pen(OutlineColor), GetVerticalScrollerRect);
            g.FillRectangle(new SolidBrush(OutlineColor), GetVerticalScrollerRect);

            if(Components.Count + Statuses.Count > MaxSimultaneousRecords)
            {
                // Draw up button
                Rectangle upButtonRect = new Rectangle(this.Width - ScrollerWidth, 0, ScrollerWidth, ScrollerWidth);
                g.DrawRectangle(new Pen(OutlineColor), upButtonRect);
                g.FillRectangle(new SolidBrush(TitleFillColor), upButtonRect);

                // Draw down button
                Rectangle downButtonRect = new Rectangle(
                    this.Width - ScrollerWidth, 
                    this.Height - ScrollerWidth, 
                    ScrollerWidth, 
                    ScrollerWidth
                );

                g.DrawRectangle(new Pen(OutlineColor), downButtonRect);
                g.FillRectangle(new SolidBrush(TitleFillColor), downButtonRect);

                // Draw scroller part

                int scrollerPartHeight = (int)((this.Height - 2 * ScrollerWidth) * ((float)MaxSimultaneousRecords / (Components.Count + Statuses.Count)));

                Rectangle scrollerPartRect = new Rectangle(
                    this.Width - ScrollerWidth, 
                    ScrollerWidth + (int)(VerticalScrollerPos * (this.Height - 2 * ScrollerWidth - scrollerPartHeight) / ((float)(Components.Count + Statuses.Count - MaxSimultaneousRecords) ) ), 
                    ScrollerWidth,
                    scrollerPartHeight
                );
                g.DrawRectangle(new Pen(OutlineColor), scrollerPartRect);
                g.FillRectangle(new SolidBrush(CyclogramTitleColor), scrollerPartRect);
            }


            // Drawing Horizontal Scroller
            g.DrawRectangle(new Pen(OutlineColor), GetHorizontalScrollerRect);
            g.FillRectangle(new SolidBrush(OutlineColor), GetHorizontalScrollerRect);

            if (Steps.Count > MaxSimultaneousSteps)
            {
                // Draw up button
                Rectangle upButtonRect = new Rectangle(0, this.Height - ScrollerWidth, ScrollerWidth, ScrollerWidth);
                g.DrawRectangle(new Pen(OutlineColor), upButtonRect);
                g.FillRectangle(new SolidBrush(TitleFillColor), upButtonRect);

                // Draw down button
                Rectangle downButtonRect = new Rectangle(
                    this.Width - ScrollerWidth,
                    this.Height - ScrollerWidth,
                    ScrollerWidth,
                    ScrollerWidth
                );

                g.DrawRectangle(new Pen(OutlineColor), downButtonRect);
                g.FillRectangle(new SolidBrush(TitleFillColor), downButtonRect);

                // Draw scroller part

                int scrollerPartWidth = (int)((this.Width - 2 * ScrollerWidth) * ((float)MaxSimultaneousSteps / (Steps.Count)));

                Rectangle scrollerPartRect = new Rectangle(
                    ScrollerWidth + (int)(HorizontalScrollerPos * (this.Width - 2 * ScrollerWidth - scrollerPartWidth) / ((float)(Steps.Count - MaxSimultaneousSteps))),
                    this.Height - ScrollerWidth,
                    scrollerPartWidth,
                    ScrollerWidth
                );
                g.DrawRectangle(new Pen(OutlineColor), scrollerPartRect);
                g.FillRectangle(new SolidBrush(CyclogramTitleColor), scrollerPartRect);
            }
        }

        private void Cyclogram_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void timer_main_Tick(object sender, EventArgs e)
        {
            if (this.Active)
            {
                IncrementTimeStamp();

                this.Refresh();
            }
        }

        private void MouseWheelScroll(object sender, MouseEventArgs e)
        {
            this.Focus();

            if(e.Delta > 0)
            {
                if(_shiftKeyPressed)
                {
                    if(HorizontalScrollerPos > 0)
                    {
                        HorizontalScrollerPos--;

                        this.Refresh();
                    }
                }
                else
                {
                    if (VerticalScrollerPos > 0)
                    {
                        VerticalScrollerPos--;

                        this.Refresh();
                    }
                }
            }
            else
            {
                if (_shiftKeyPressed)
                {
                    if (HorizontalScrollerPos < Steps.Count - MaxSimultaneousSteps)
                    {
                        HorizontalScrollerPos++;

                        this.Refresh();
                    }
                }
                else
                {
                    if (VerticalScrollerPos < Components.Count + Statuses.Count - MaxSimultaneousRecords)
                    {
                        VerticalScrollerPos++;

                        this.Refresh();
                    }
                }
            }
        }

        private void Cyclogram_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift)
            {
                _shiftKeyPressed = true;
            }
        }

        private void Cyclogram_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift)
            {
                _shiftKeyPressed = false;
            }
        }
    }
}
