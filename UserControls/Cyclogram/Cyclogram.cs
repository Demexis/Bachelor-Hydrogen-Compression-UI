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
            }
        }

        public bool VerticalScrollerFollowMouse;
        private Point _verticalMouseStartLocation;
        private int _verticalPosWhenMouseStarted;
        public bool HorizontalScrollerFollowMouse;
        private Point _horizontalMouseStartLocation;
        private int _horizontalPosWhenMouseStarted;

        public Action<string, string> OnComponentStatusChange;

        [Category("Appearance"), Description("Name of the cyclogram.")]
        public string CyclogramName { get; set; }

        public enum CyclogramPlayMode { Single, Loop };

        [Category("Behavior"), Description("Play mode defines if the cyclogram's execution will stop after reaching the end.")]
        public CyclogramPlayMode PlayMode { get { return _playMode; } set { _playMode = value; } }

        private CyclogramPlayMode _playMode;

        public int CurrentTimeStamp { get; set; }

        public void SetCurrentTimeStamp(int valueMilliseconds, bool checkSteps = true) 
        { 
            CurrentTimeStamp = valueMilliseconds;
            UpdateVisionPos();
            if(checkSteps) CheckSteps();
            this.Refresh(); 
        }

        public List<CyclogramComponentElement> Components = new List<CyclogramComponentElement>();
        public List<CyclogramStatusElement> Statuses = new List<CyclogramStatusElement>();
        public List<CyclogramStepElement> Steps = new List<CyclogramStepElement>();

        public float TitleWidthRatio = 0.2f;
        public float CyclogramRulerHeightRatio = 0.1f;

        public int VerticalScrollerPos = 0;

        public int ScrollerWidth = 20;

        public int MaxSimultaneousRecords = 20;

        private bool _shiftKeyPressed;

        public Action OnSingleExecutionEnd;

        public int VisionPos = 0;
        public int VisionRange = 2000; // Milliseconds

        public int FollowSleepTime = 2000;
        public int _followStopTime = 0;

        private float _verticalPosScroll = 0.1f; // [0 - 1]
        public float VerticalPosScroll
        {
            get { return _verticalPosScroll; }
            set { _verticalPosScroll = Mathf.Clamp(value, 0, 1); }
        }

        private float _timeStampFollowPoint = 0.5f; // [0 - 1]
        public float TimeStampFollowPoint { 
            get { return _timeStampFollowPoint; } 
            set { _timeStampFollowPoint = Mathf.Clamp(value, 0, 1); } 
        } 

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

        public Color ActiveOutlineColor = Color.FromArgb(200, 150, 50);

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

        public void Stop()
        {
            this.Active = false;
            this.CurrentTimeStamp = 0;
            OnSingleExecutionEnd?.Invoke();
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

            UpdateVisionPos();

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

            UpdateVisionPos();

            this.Refresh();
        }

        public void SetRightEnd()
        {
            int traveledLength = this.Steps.Count - 1 > 0 ? Steps.Take(this.Steps.Count - 1).Sum((x) => x.LengthMilliseconds) : 0;

            this.CurrentTimeStamp = traveledLength;
            this.CheckSteps();

            UpdateVisionPos();

            this.Refresh();
        }

        public void SetLeftEnd()
        {
            this.CurrentTimeStamp = 0;
            this.CheckSteps();

            UpdateVisionPos();

            this.Refresh();
        }

        public void Clear()
        {
            Components.Clear();
            Statuses.Clear();
            Steps.Clear();
        }

        #endregion

        private void IncrementTimeStamp()
        {
            if (this.CurrentTimeStamp >= this.GetTotalLengthInMilliseconds - 1)
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

            if (this.CurrentTimeStamp + timer_main.Interval >= this.GetTotalLengthInMilliseconds - 1)
            {
                this.CurrentTimeStamp = this.GetTotalLengthInMilliseconds - 1;
            }
            else
            {
                this.CurrentTimeStamp += timer_main.Interval;
            }

            if (this.CurrentTimeStamp >= this.GetTotalLengthInMilliseconds - 1 &&
                this.PlayMode == Cyclogram.CyclogramPlayMode.Single)
            {
                Stop();

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

                        //Console.WriteLine($"Sequence [{sequence.SequenceID}] has been {(sequence.Active ? "activated" : "deactivated")}");

                        foreach (CyclogramStatusElement title in this.Statuses)
                        {
                            if (title.TitleID.Equals(sequence.TitleID))
                            {
                                //Console.WriteLine($"Title [{title.Text}] has been {(sequence.Active ? "activated" : "deactivated")}");

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

        private void UncheckSteps()
        {
            foreach(CyclogramStepElement step in this.Steps)
            {
                foreach (CyclogramSequenceElement sequence in step.Sequences)
                {
                    sequence.Active = false;
                }
            }
        }

        

        #region Events

        private void Cyclogram_Paint(object sender, PaintEventArgs e)
        {
            if (Steps.Count == 0) return;

            Graphics g = e.Graphics;

            Rectangle MainTitleRect = GetCyclogramTitleRect;
            Rectangle StepsRect = GetStepsRect;
            Rectangle TitlesRect = GetTitlesRect;
            Rectangle SequencesRect = GetSequencesRect;


            // Drawing Background
            g.FillRectangle(new SolidBrush(BackgroundColor), new Rectangle(default(Point), this.Size));

            // Drawing Cyclogram Name
            {
                g.FillRectangle(new SolidBrush(CyclogramTitleColor), new RectangleF(MainTitleRect.Location, MainTitleRect.Size));
                g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(MainTitleRect.Location, MainTitleRect.Size));
                g.DrawString(" " + CyclogramName, Font, new SolidBrush(BrightTextColor), MainTitleRect.Location);
            }

            // Setting vision range
            
            if(_followStopTime <= 0 && this.Active)
            {
                UpdateVisionPos();
            }

            int visionStartPos = Mathf.Clamp(VisionPos + VisionRange, 0, GetTotalLengthInMilliseconds) - VisionRange;
            int visionEndPos = Mathf.Clamp(visionStartPos + VisionRange, 0, GetTotalLengthInMilliseconds);

            // Drawing Steps

            Dictionary<CyclogramStepElement, Point> stepLines = new Dictionary<CyclogramStepElement, Point>();
            {
                Point stepsLocation = StepsRect.Location;

                //int lineWidth = StepsRect.Width;


                for (int i = 0; i < Steps.Count; i++)
                {
                    int localPos = 0;

                    for (int j = 0; j < i; j++)
                    {
                        localPos += Steps[j].LengthMilliseconds;
                    }

                    // If step is within the range
                    if (Mathf.LineSegmentsIntersect(localPos, localPos + Steps[i].LengthMilliseconds, visionStartPos, visionEndPos))
                    {
                        (int x, int y) = Mathf.LineSegmentsConjunction(localPos, localPos + Steps[i].LengthMilliseconds, visionStartPos, visionEndPos);

                        int operationWidth = (int)((y - x) / (float)(visionEndPos - visionStartPos) * StepsRect.Width);

                        int operationX = StepsRect.X + (int)((x - visionStartPos) / (float)(visionEndPos - visionStartPos) * StepsRect.Width);

                        g.FillRectangle(new SolidBrush(CategoryFillColor), new RectangleF(operationX, stepsLocation.Y, operationWidth, StepsRect.Height));
                        g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(operationX, stepsLocation.Y, operationWidth, StepsRect.Height));
                        g.DrawString(" " + Steps[i].Name, Font, new SolidBrush(BrightTextColor), new Point(operationX, stepsLocation.Y));

                        stepLines.Add(Steps[i], new Point(operationX, operationWidth));
                    }
                }
            }

            // Drawing roads
            {
                int height = TitlesRect.Height;

                int segments = 4 * Steps.Count;

                Point location = SequencesRect.Location;

                for (int i = 0; i < MaxSimultaneousRecords; i++)
                {
                    int lineWidth = SequencesRect.Width;

                    int cellHeight = height / (MaxSimultaneousRecords - i);
                    height -= cellHeight;

                    for (int j = 0; j < segments; j++)
                    {
                        int segmentWidth = lineWidth / (segments - j);
                        lineWidth -= segmentWidth;

                        if (j % 2 == 0)
                            g.FillRectangle(new SolidBrush(SecondBackgroundColor), new Rectangle(location, new Size(segmentWidth, cellHeight)));

                        g.DrawRectangle(new Pen(OutlineColor), new Rectangle(location, new Size(segmentWidth, cellHeight)));
                        location.X += segmentWidth;
                    }
                    location.X = SequencesRect.Left;
                    location.Y += cellHeight;
                }
            }

            int countOfRecords = 0;
            int countOfSkips = VerticalScrollerPos;

            // Drawing Categories and Titles
            {
                Point location = TitlesRect.Location;

                int width = TitlesRect.Width;
                int height = TitlesRect.Height;

                for (int i = 0; i < Components.Count; i++)
                {
                    CyclogramComponentElement component = Components[i];

                    if (countOfSkips == 0)
                    {
                        if (countOfRecords >= MaxSimultaneousRecords) break;

                        countOfRecords++;

                        int cellHeight = height / (MaxSimultaneousRecords - countOfRecords + 1);
                        height -= cellHeight;

                        g.FillRectangle(new SolidBrush(CategoryFillColor), new RectangleF(location.X, location.Y, width, cellHeight));
                        g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(location.X, location.Y, width, cellHeight));
                        g.DrawString(" " + component.Name, Font, new SolidBrush(BrightTextColor), location + new Size(0, cellHeight / 4));

                        int rectSide = width > cellHeight ? cellHeight / 5 : width / 5;

                        Point p = new Point(location.X + width - rectSide, location.Y);
                        g.DrawRectangle(new Pen(OutlineColor), new Rectangle(p, new Size(rectSide, cellHeight)));
                        g.FillRectangle(new SolidBrush(CyclogramTitleColor), new Rectangle(p, new Size(rectSide, cellHeight)));

                        location.Y += cellHeight;
                    }
                    else
                    {
                        countOfSkips--;
                    }

                    

                    foreach (CyclogramStatusElement title in component.Titles)
                    {
                        if(countOfSkips > 0)
                        {
                            countOfSkips--;
                            continue;
                        }

                        if (countOfRecords >= MaxSimultaneousRecords) break;

                        countOfRecords++;

                        int cellHeight = height / (MaxSimultaneousRecords - countOfRecords + 1);
                        height -= cellHeight;

                        g.FillRectangle(new SolidBrush(TitleFillColor), new RectangleF(location.X, location.Y, width, cellHeight));
                        g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(location.X, location.Y, width, cellHeight));
                        g.DrawString("   " + title.Text, Font, new SolidBrush(DefaultTextColor), location + new Size(0, cellHeight / 4));

                        // Drawing sequences

                        for (int k = 0; k < Steps.Count; k++)
                        {
                            if(stepLines.TryGetValue(Steps[k], out Point stepLine))
                            {
                                int currentCellPos = stepLine.X;
                                int currentCellWidth = stepLine.Y;

                                for (int k3 = 0; k3 < Steps[k].Sequences.Count; k3++)
                                {
                                    if (!Steps[k].Sequences[k3].TitleID.Equals(title.TitleID)) continue;

                                    LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                       location + new Size(currentCellPos, 0),
                                       location + new Size(currentCellPos, 0) + new Size(currentCellWidth, cellHeight),
                                       CyclogramTitleColor,
                                       DefaultTextColor);

                                    g.DrawRectangle(new Pen(OutlineColor, 2), new Rectangle(location + new Size(currentCellPos, 0), new Size(currentCellWidth, cellHeight)));
                                    g.FillRectangle(linGrBrush, new Rectangle(location + new Size(currentCellPos, 0), new Size(currentCellWidth, cellHeight)));


                                    if (Steps[k].Sequences[k3].Active)
                                    {
                                        g.DrawRectangle(new Pen(ActiveOutlineColor, 2), new Rectangle(location + new Size(currentCellPos, 0), new Size(currentCellWidth, cellHeight)));
                                    }
                                }
                            }
                        }

                        location.Y += cellHeight;
                    }
                }
            }

            // Drawing Time Stamp line

            Point timeStampLocation;

            if (GetTotalLengthInMilliseconds != 0)
            {
                if (Mathf.Between(CurrentTimeStamp, visionStartPos, visionEndPos))
                {
                    timeStampLocation = new Point(SequencesRect.Left + (int)(SequencesRect.Width * Mathf.NormalizedRelationBetween(CurrentTimeStamp, visionStartPos, visionEndPos)), SequencesRect.Top);

                    g.DrawRectangle(new Pen(ActiveOutlineColor, 2), new Rectangle(timeStampLocation, new Size(1, SequencesRect.Height)));
                }
            }
            else
            {
                timeStampLocation = new Point(SequencesRect.Left, SequencesRect.Top);
                g.DrawRectangle(new Pen(ActiveOutlineColor, 2), new Rectangle(timeStampLocation, new Size(1, SequencesRect.Height)));
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

            if (GetTotalLengthInMilliseconds > VisionRange)
            {
                // Draw left button
                Rectangle upButtonRect = new Rectangle(0, this.Height - ScrollerWidth, ScrollerWidth, ScrollerWidth);
                g.DrawRectangle(new Pen(OutlineColor), upButtonRect);
                g.FillRectangle(new SolidBrush(TitleFillColor), upButtonRect);

                // Draw right button
                Rectangle downButtonRect = new Rectangle(
                    this.Width - ScrollerWidth,
                    this.Height - ScrollerWidth,
                    ScrollerWidth,
                    ScrollerWidth
                );

                g.DrawRectangle(new Pen(OutlineColor), downButtonRect);
                g.FillRectangle(new SolidBrush(TitleFillColor), downButtonRect);

                // Draw scroller part

                int scrollerPartWidth = (int)((this.Width - 2 * ScrollerWidth) * (VisionRange / (float)(GetTotalLengthInMilliseconds)));

                Rectangle scrollerPartRect = new Rectangle(
                    ScrollerWidth + (int)(visionStartPos / (float)(GetTotalLengthInMilliseconds - VisionRange) * (this.Width - 2 * ScrollerWidth - scrollerPartWidth)),
                    this.Height - ScrollerWidth,
                    scrollerPartWidth,
                    ScrollerWidth
                );
                g.DrawRectangle(new Pen(OutlineColor), scrollerPartRect);
                g.FillRectangle(new SolidBrush(CyclogramTitleColor), scrollerPartRect);
            }
        }

        public void UpdateVisionPos()
        {
            if (CurrentTimeStamp > VisionPos + (int)(VisionRange * TimeStampFollowPoint))
            {
                VisionPos = CurrentTimeStamp - (int)(VisionRange * TimeStampFollowPoint);
            }

            if (VisionPos > CurrentTimeStamp)
            {
                VisionPos = CurrentTimeStamp;
            }
        }

        private void Cyclogram_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void timer_main_Tick(object sender, EventArgs e)
        {
            _followStopTime -= timer_main.Interval;

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
                    VisionPos = Mathf.Clamp(VisionPos - (int)(VisionRange * _verticalPosScroll), 0, GetTotalLengthInMilliseconds - VisionRange);

                    _followStopTime = FollowSleepTime;

                    this.Refresh();

                    //if (HorizontalScrollerPos > 0)
                    //{
                    //    HorizontalScrollerPos--;

                    //    this.Refresh();
                    //}
                }
                else
                {
                    if (VerticalScrollerPos > 0)
                    {
                        VerticalScrollerPos--;

                        if (_followStopTime > 0) _followStopTime = FollowSleepTime;

                        this.Refresh();
                    }
                }
            }
            else
            {
                if (_shiftKeyPressed)
                {
                    VisionPos = Mathf.Clamp(VisionPos + (int)(VisionRange * _verticalPosScroll), 0, GetTotalLengthInMilliseconds - VisionRange);

                    _followStopTime = FollowSleepTime;

                    this.Refresh();

                    //if (HorizontalScrollerPos < Steps.Count - MaxSimultaneousSteps)
                    //{
                    //    HorizontalScrollerPos++;

                    //    this.Refresh();
                    //}
                }
                else
                {
                    if (VerticalScrollerPos < Components.Count + Statuses.Count - MaxSimultaneousRecords)
                    {
                        VerticalScrollerPos++;

                        if (_followStopTime > 0) _followStopTime = FollowSleepTime;

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




        #endregion

        private void Cyclogram_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MOUSE DOWN");

            // Vertical Scroller
            int scrollerPartHeight = (int)((this.Height - 2 * ScrollerWidth) * ((float)MaxSimultaneousRecords / (Components.Count + Statuses.Count)));

            Rectangle verticalScrollerPartRect = new Rectangle(
                    this.Width - ScrollerWidth,
                    ScrollerWidth + (int)(VerticalScrollerPos * (this.Height - 2 * ScrollerWidth - scrollerPartHeight) / ((float)(Components.Count + Statuses.Count - MaxSimultaneousRecords))),
                    ScrollerWidth,
                    scrollerPartHeight
                );

            if (verticalScrollerPartRect.Contains(e.Location) && Components.Count + Statuses.Count > MaxSimultaneousRecords)
            {
                Console.WriteLine("MOUSE DOWN VERTICAL SET");

                VerticalScrollerFollowMouse = true;
                _verticalMouseStartLocation = e.Location;
                _verticalPosWhenMouseStarted = VerticalScrollerPos;
            }

            // Horizontal Scroller
            int visionStartPos = Mathf.Clamp(VisionPos + VisionRange, 0, GetTotalLengthInMilliseconds) - VisionRange;
            int scrollerPartWidth = (int)((this.Width - 2 * ScrollerWidth) * (VisionRange / (float)(GetTotalLengthInMilliseconds)));

            Rectangle horizontalScrollerPartRect = new Rectangle(
                ScrollerWidth + (int)(visionStartPos / (float)(GetTotalLengthInMilliseconds - VisionRange) * (this.Width - 2 * ScrollerWidth - scrollerPartWidth)),
                this.Height - ScrollerWidth,
                scrollerPartWidth,
                ScrollerWidth
            );

            if (horizontalScrollerPartRect.Contains(e.Location))
            {
                HorizontalScrollerFollowMouse = true;
                _horizontalMouseStartLocation = e.Location;
            }
        }

        private void Cyclogram_MouseUp(object sender, MouseEventArgs e)
        {
            VerticalScrollerFollowMouse = false;
            HorizontalScrollerFollowMouse = false;
        }

        private void Cyclogram_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MOVES");

            if(VerticalScrollerFollowMouse)
            {
                Console.WriteLine("MOVES VERTICAL");

                int scrollerPartHeight = (int)((this.Height - 2 * ScrollerWidth) * ((float)MaxSimultaneousRecords / (Components.Count + Statuses.Count)));
                int step = scrollerPartHeight / MaxSimultaneousRecords;

                int y = _verticalPosWhenMouseStarted + (e.Location.Y - _verticalMouseStartLocation.Y) / step;

                VerticalScrollerPos = Mathf.Clamp(y, 0, Components.Count + Statuses.Count - MaxSimultaneousRecords);

                Console.WriteLine("VERTICAL POS " + VerticalScrollerPos);

                this.Refresh();
            }

            if(HorizontalScrollerFollowMouse)
            {
                Console.WriteLine("MOVES HORIZONTAL");

                int scrollerPartWidth = (int)((this.Width - 2 * ScrollerWidth) * (VisionRange / (float)(GetTotalLengthInMilliseconds)));
                float step = (float)scrollerPartWidth / VisionRange;

                int x = (int)(_horizontalPosWhenMouseStarted + (e.Location.X - _horizontalMouseStartLocation.X) / step);

                VisionPos = Mathf.Clamp(x, 0, GetTotalLengthInMilliseconds - VisionRange);

                Console.WriteLine("HORIZONTAL POS " + VisionPos);

                this.Refresh();
            }
        }
    }
}
