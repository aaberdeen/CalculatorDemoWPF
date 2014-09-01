using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
       // static MyTextBox DisplayBox;
        static MyTextBox PaperBox;
        static PaperTrail Paper;
        

        public Window1()
            : base()
        {
            InitializeComponent();
            //Binding buttonText = new Binding();
            //buttonText.Source = ByPowerX;
            //buttonText. = "y^x";

            //sub-class our textBox
            //DisplayBox = new MyTextBox();
            //Grid.SetRow(DisplayBox, 0);
            //Grid.SetColumn(DisplayBox, 5);
            //Grid.SetColumnSpan(DisplayBox, 29);
            //DisplayBox.Height = 30;
            //MyGrid.Children.Add(DisplayBox);

            //sub-class our paper trail textBox
            PaperBox = new MyTextBox();
            Grid.SetRow(PaperBox, 5);
            Grid.SetColumn(PaperBox, 0);
            Grid.SetColumnSpan(PaperBox, 4);
            Grid.SetRowSpan(PaperBox, 13);
            PaperBox.IsReadOnly = true;
            PaperBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            PaperBox.Margin = new Thickness(3.0,1.0,1.0,1.0);
            PaperBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            Paper = new PaperTrail();

            MyGrid.Children.Add(PaperBox);
            ProcessKey('0');
            EraseDisplay = true;

        }

        private enum Operation
        {
            None,
            Devide,
            Multiply,
            Subtract,
            Add,
            Percent,
            Sqrt,
            Squr,
            Cube,
            CubeRt,
            OneX,
            yPowerX,
            yRootX,
            Sin,
            Cos,
            Tan,
            Sinh,
            Cosh,
            Tanh,
            ln,
            log,
            Negate
        }
        private Operation LastOper;
        private Operation EqualsOper;
        private string _display;
        private string _exptop;
        private string _last_val;
        private string _mem_val;
        private string _sbase;
        private bool _erasediplay;
        private bool _hyp;
        private bool _exp;



        private string sBase
        {
            get
            {
                return _sbase;
            }
            set
            {
                _sbase = value;
                long boxValue;
                switch (value)
                {
                    case "DEC":
                        FuncBoxHex.Text = "";
                        FuncBoxOct.Text = "";
                        FuncBoxBin.Text = "";
                        ByPowerX.Content = "X^Y";
                        ByPowerX.Click -= DigitBtn_Click;
                        ByPowerX.Click += OperBtn_Click;
                        BSqrt.Content = "√";
                        BSqrt.Click -= DigitBtn_Click;
                        BSqrt.Click += OperBtn_Click;
                        BSqur.Content = "X²";
                        BSqur.Click -= DigitBtn_Click;
                        BSqur.Click += OperBtn_Click;
                        Bhyp.Content = "hyp";
                        Bhyp.Click -= DigitBtn_Click;
                        Bhyp.Click += OperBtn_Click;
                        Bsin.Content = "sin";
                        Bsin.Click -= DigitBtn_Click;
                        Bsin.Click += OperBtn_Click;
                        Bcos.Content = "cos";
                        Bcos.Click -= DigitBtn_Click;
                        Bcos.Click += OperBtn_Click;
                        BPeriod.IsEnabled = true;
                        
                        boxValue = Convert.ToInt64(Display, 16);
                        // boxValue = int.Parse(Display, System.Globalization.NumberStyles.HexNumber);
                        Display = boxValue.ToString();
                        UpdateDisplay();
                        break;
                    case "HEX":
                        FuncBoxHex.Text = "HEX";
                        FuncBoxOct.Text = "";
                        FuncBoxBin.Text = "";
                        ByPowerX.Content = "A";
                        ByPowerX.Click -= OperBtn_Click;
                        ByPowerX.Click += DigitBtn_Click;
                        BSqrt.Content = "B";
                        BSqrt.Click -= OperBtn_Click;
                        BSqrt.Click += DigitBtn_Click;
                        BSqur.Content = "C";
                        BSqur.Click -= OperBtn_Click;
                        BSqur.Click += DigitBtn_Click;
                        Bhyp.Content = "D";
                        Bhyp.Click -= OperBtn_Click;
                        Bhyp.Click += DigitBtn_Click;
                        Bsin.Content = "E";
                        Bsin.Click -= OperBtn_Click;
                        Bsin.Click += DigitBtn_Click;
                        Bcos.Content = "F";
                        Bcos.Click -= OperBtn_Click;
                        Bcos.Click += DigitBtn_Click;
                        BPeriod.IsEnabled = false;

                        boxValue = long.Parse(Display);
                        Display = boxValue.ToString("X");
                        UpdateDisplay();
                        break;

                    case "OCT":
                        FuncBoxHex.Text = "";
                        FuncBoxOct.Text = "OCT";
                        FuncBoxBin.Text = "";
                        break;
                    case "BIN":
                        FuncBoxHex.Text = "";
                        FuncBoxOct.Text = "";
                        FuncBoxBin.Text = "BIN";
                        break;                        
                }
                
            }
        }

        private bool Exp
        {
            get
            {
                return _exp;
            }
            set
            {
                _exp = value;
            }
        }

        private bool Hyp
        {
            get
            {
                return _hyp;
            }
            set
            {
                _hyp = value;
            }


        }

        //flag to erase or just add to current display flag
        private bool EraseDisplay
        {
            get
            {
                return _erasediplay;

            }
            set
            {
                _erasediplay = value;
            }
        }
        //Get/Set Memory cell value
        private Double Memory
        {
            get
            {
                if (_mem_val == string.Empty)
                    return 0.0;
                else
                    return Convert.ToDouble(_mem_val);
            }
            set
            {
                _mem_val = value.ToString();
            }
        }
        //Lats value entered
        private string LastValue
        {
            get
            {
                if (_last_val == string.Empty)
                    return "0";
                return _last_val;

            }
            set
            {
                _last_val = value;
            }
        }
        //The current Calculator display
        private string Display
        {
            get
            {
                return _display;
            }
            set
            {
                _display = value;
            }
        }
        private string ExpTop
        {
            get
            {
                return _exptop;
            }
            set
            {
                _exptop = value;
            }
        }

        private string DisplayExp
        {
            get
            {
             double result;
                if (ExpTop != "")
                {
                    result = Convert.ToDouble(Display) * (Math.Pow(10,Convert.ToDouble(ExpTop)));
                }
                else
                {
                    if (sBase == "HEX")
                    {
                        //boxValue = Convert.ToInt64(Display, 16);
                        result = Convert.ToInt64(Display,16);
                    }
                    else
                    {
                        result = Convert.ToDouble(Display);
                    }
                }
            return Convert.ToString(result); 
            }
        }

        // Sample event handler:  
        private void OnWindowKeyDown(object sender, System.Windows.Input.TextCompositionEventArgs /*System.Windows.Input.KeyEventArgs*/ e)
        {
            string s = e.Text;
            char c = (s.ToCharArray())[0];
            e.Handled = true;

            if ((c >= '0' && c <= '9') || c == '.' || c == '\b')  // '\b' is backspace
            {
                ProcessKey(c);
                return;
            }
            switch (c)
            {
                case '+':
                    ProcessOperation("BPlus");
                    break;
                case '-':
                    ProcessOperation("BMinus");
                    break;
                case '*':
                    ProcessOperation("BMultiply");
                    break;
                case '/':
                    ProcessOperation("BDevide");
                    break;
                case '%':
                    ProcessOperation("BPercent");
                    break;
                case '=':
                    ProcessOperation("BEqual");
                    break;
                case '\r':
                    ProcessOperation("BEqual");
                    break;
            }

        }
        private void DigitBtn_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Content.ToString();

            //char[] ids = ((Button)sender).ID.ToCharArray();
            char[] ids = s.ToCharArray();

            if (Exp == false)
            {
                ProcessKey(ids[0]);
            }
            else
            {
                ProcessKeyExp(ids[0]);
            }

        }

        private void ProcessKeyExp(char c)
        {
            //if (EraseDisplay)
            //{
            //    Display = string.Empty;
            //    EraseDisplay = false;
            //}
            AddToDisplayExp(c);
        }
        private void ProcessKey(char c)
        {
            if (EraseDisplay)
            {
                Display = string.Empty;
                ExpTop = string.Empty;
                EraseDisplay = false;
            }
            AddToDisplay(c);
            UpdateDisplayExp();
            
        }

        private void DigitBtn_ClickRight(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Content.ToString();

            //char[] ids = ((Button)sender).ID.ToCharArray();
            char[] ids = s.ToCharArray();
            ProcessKeyShift(ids[0]);

        }

        private void ProcessKeyShift(char c)
        {
            if (EraseDisplay)
            {
                Display = string.Empty;
                EraseDisplay = false;
            }
            switch (c)
            {
                case '1':
                    Display = "3.141592654";
                    UpdateDisplay();
                    break;
                case '2' :
                    Display = "2.718281828";
                    UpdateDisplay();
                    break;
            }
        }

        private void ProcessOperation(string s)
        {
            Double d = 0.0;
            switch (s)
            {
                case "Bln":
                    LastOper = Operation.ln;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    
                    break;
                case "Blog":
                    LastOper = Operation.log;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    
                    break;
                case "BExp":
                    Exp = true;
                    ExpBoxBot.Text = "x10";
                    break;
                case "Bhyp":
                    Hyp = true;
                    break;
                case "Btan":
                    LastOper = Operation.Tan;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "Bcos":
                    LastOper = Operation.Cos;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "Bsin":
                    LastOper = Operation.Sin;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;

                case "ByPowerX":
                    if (EraseDisplay)
                    {  //stil wait for a digit...
                        LastOper = Operation.yPowerX;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.yPowerX;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;

                case "BPM":
                    LastOper = Operation.Negate;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "BDevide":

                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Devide;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Devide;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BMultiply":
                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Multiply;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Multiply;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BMinus":
                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Subtract;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Subtract;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BPlus":
                    if (EraseDisplay)
                    {  //stil wait for a digit...
                        LastOper = Operation.Add;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Add;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BEqual":
                    //if (EraseDisplay)    //stil wait for a digit...
                    //    break;
                    if (LastOper == Operation.None)         // to handel multiple equals presses
                    {
                        LastOper = EqualsOper;
                    }
                    CalcResults();
                    EraseDisplay = true;
                    EqualsOper = LastOper;
                    LastOper = Operation.None;
                    LastValue = DisplayExp;
                    ExpTop = string.Empty;
                    UpdateDisplayExp();
                    //val = Display;
                    break;
                case "BSqrt":
                    LastOper = Operation.Sqrt;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "BSqur":
                    LastOper = Operation.Squr;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "BPercent":
                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Percent;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Percent;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    //LastOper = Operation.None;
                    break;
                case "BOneOver":
                    LastOper = Operation.OneX;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "BC":  //clear All
                    LastOper = Operation.None;
                    Display = LastValue = string.Empty;
                    Paper.Clear();
                    UpdateDisplay();
                    break;
                case "BCE":  //clear entry
                    LastOper = Operation.None;
                    Display = LastValue;
                    UpdateDisplay();
                    break;
                case "BMemClear":
                    Memory = 0.0F;
                    DisplayMemory();
                    break;
                case "BMemSave":
                    Memory = Convert.ToDouble(DisplayExp);
                    DisplayMemory();
                    EraseDisplay = true;
                    break;
                case "BMemRecall":
                    Display = /*val =*/ Memory.ToString();
                    UpdateDisplay();
                    //if (LastOper != Operation.None)   //using MR is like entring a digit
                    EraseDisplay = false;
                    break;
                case "BMemPlus":
                    d = Memory + Convert.ToDouble(DisplayExp);
                    Memory = d;
                    DisplayMemory();
                    EraseDisplay = true;
                    break;
            }

        }

        private void ProcessOperationShift(string s)
        {
            Double d = 0.0;
            switch (s)
            {


             
                 
                case "ByPowerX":
                    if (EraseDisplay)
                    {  //stil wait for a digit...
                        LastOper = Operation.yRootX;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.yRootX;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BPM":
                    LastOper = Operation.Negate;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "BDevide":

                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Devide;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Devide;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BMultiply":
                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Multiply;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Multiply;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    break;
                case "BMinus":
                    sBase = "HEX";
                    break;
                case "BPlus":
                    sBase = "DEC";
                    break;
                case "BEqual":
                    if (EraseDisplay)    //stil wait for a digit...
                        break;
                    CalcResults();
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    LastValue = DisplayExp;
                    //val = Display;
                    break;
                case "BSqrt":
                    LastOper = Operation.CubeRt;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
                case "BSqur":
                    LastOper = Operation.Cube;
                    LastValue = DisplayExp;
                    CalcResults();
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    LastOper = Operation.None;
                    break;
               // case "BPercent":
                    
                case "BOneOver":
                    if (EraseDisplay)    //stil wait for a digit...
                    {  //stil wait for a digit...
                        LastOper = Operation.Percent;
                        break;
                    }
                    CalcResults();
                    LastOper = Operation.Percent;
                    LastValue = DisplayExp;
                    EraseDisplay = true;
                    //LastOper = Operation.None;
                    break;
                case "BC":  //clear All
                    LastOper = Operation.None;
                    Display = LastValue = string.Empty;
                    Paper.Clear();
                    UpdateDisplay();
                    break;
                case "BCE":  //clear entry
                    LastOper = Operation.None;
                    Display = LastValue;
                    UpdateDisplay();
                    break;
                case "BMemClear":
                    Memory = 0.0F;
                    DisplayMemory();
                    break;
                case "BMemSave":
                    Memory = Convert.ToDouble(DisplayExp);
                    DisplayMemory();
                    EraseDisplay = true;
                    break;
                case "BMemRecall":
                    Display = /*val =*/ Memory.ToString();
                    UpdateDisplay();
                    //if (LastOper != Operation.None)   //using MR is like entring a digit
                    EraseDisplay = false;
                    break;
                case "BMemPlus":
                    d = Memory + Convert.ToDouble(DisplayExp);
                    Memory = d;
                    DisplayMemory();
                    EraseDisplay = true;
                    break;
            }

        }

        private void OperBtn_Click(object sender, RoutedEventArgs e)
        {
            Exp = false;
            ProcessOperation(((Button)sender).Name.ToString());
       }

        private void OperBtn_RightClick(object sender, RoutedEventArgs e)
        {
            Exp = false;
            ProcessOperationShift(((Button)sender).Name.ToString());
        }


        private double Calc(Operation LastOper)
        {
            double d = 0.0;


            try {
            switch (LastOper)
            {

                case Operation.ln:
                    Paper.AddArguments(" ln " + LastValue);
                    d = Math.Log(Convert.ToDouble(LastValue));
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.log:
                    Paper.AddArguments(" log " + LastValue);
                    d = Math.Log10(Convert.ToDouble(LastValue));
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Tan:
                    if (Hyp == false)
                    {
                        if ((Convert.ToDouble(LastValue) + 90) % 180 == 0)
                        {
                            throw new Exception("Illegal value");
                        }
                        Paper.AddArguments(" tan( " + LastValue + ")");
                        //  double rad = Math.PI * Convert.ToDouble(LastValue) / 180.0;
                        d = Math.Round(Math.Tan(Math.PI * Convert.ToDouble(LastValue) / 180.0), 15);
                    }
                    if (Hyp == true)
                    {
                        Paper.AddArguments(" tanh( " + LastValue + ")");
                        //  double rad = Math.PI * Convert.ToDouble(LastValue) / 180.0;
                        d = Math.Round(Math.Tanh(Convert.ToDouble(LastValue)), 15);
                        Hyp = false;
                    }                    
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                
                case Operation.Cos:
                    if (Hyp == false)
                    {
                    Paper.AddArguments(" cos( " + LastValue + ")");
                    //  double rad = Math.PI * Convert.ToDouble(LastValue) / 180.0;
                    d = Math.Round(Math.Cos(Math.PI * Convert.ToDouble(LastValue) / 180.0), 15);
                    }
                    if (Hyp == true)
                    {
                     Paper.AddArguments("cosh( " + LastValue + ")");
                    //  double rad = Math.PI * Convert.ToDouble(LastValue) / 180.0;
                    d = Math.Round(Math.Cosh(Convert.ToDouble(LastValue)), 15);
                    Hyp = false;
                    }
                                     
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;

                case Operation.Sin:
                    if (Hyp == false)
                    {
                        Paper.AddArguments("sin( " + LastValue + ")");
                        d = Math.Round(Math.Sin(Math.PI * Convert.ToDouble(LastValue) / 180.0), 15);
                    }
                    if (Hyp == true)
                    {
                        Paper.AddArguments("sinh( " + LastValue + ")");
                        d = Math.Round(Math.Sinh(Convert.ToDouble(LastValue)), 15);
                        Hyp = false;
                    }
                    CheckResult(d);
                    Paper.AddResult(d.ToString());                    
                    break;


                case Operation.CubeRt:
                    Paper.AddArguments(DisplayExp + " √ " + LastValue);
                    d = Math.Pow(Convert.ToDouble(LastValue), 1.0/3.0);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.yRootX:
                    Paper.AddArguments(DisplayExp + " √ " + LastValue);
                    d = Math.Pow(Convert.ToDouble(LastValue), 1 / (Convert.ToDouble(DisplayExp)));
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.yPowerX:
                    Paper.AddArguments(LastValue + " ^ " + DisplayExp);
                    d = Math.Pow(Convert.ToDouble(LastValue), Convert.ToDouble(DisplayExp));
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Devide:
                    Paper.AddArguments(LastValue + " / " + DisplayExp);
                    d = (Convert.ToDouble(LastValue) / Convert.ToDouble(DisplayExp));
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Add:
                    Paper.AddArguments(LastValue + " + " + DisplayExp);
                    d = Convert.ToDouble(LastValue) + Convert.ToDouble(DisplayExp);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Multiply:
                    Paper.AddArguments(LastValue + " * " + DisplayExp);
                    d = Convert.ToDouble(LastValue) * Convert.ToDouble(DisplayExp);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Percent:
                    //Note: this is different (but make more sense) then Windows calculator
                    Paper.AddArguments(LastValue + " % " + DisplayExp);
                    d = (Convert.ToDouble(LastValue) * Convert.ToDouble(DisplayExp)) / 100.0F;
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Subtract:
                    Paper.AddArguments(LastValue + " - " + DisplayExp);
                    d = Convert.ToDouble(LastValue) - Convert.ToDouble(DisplayExp);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Sqrt:
                    Paper.AddArguments("Sqrt( " + DisplayExp + " )");
                    d = Math.Sqrt(Convert.ToDouble(DisplayExp));
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Squr:
                    Paper.AddArguments("Squr( " + DisplayExp + " )");
                    d = Convert.ToDouble(Display) * Convert.ToDouble(DisplayExp);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Cube:
                    Paper.AddArguments("Cube( " + DisplayExp + " )");
                    d = Convert.ToDouble(DisplayExp) * Convert.ToDouble(DisplayExp) * Convert.ToDouble(DisplayExp);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;

                case Operation.OneX:
                    Paper.AddArguments("1 / " + LastValue);
                    d = 1.0F / Convert.ToDouble(LastValue);
                    CheckResult(d);
                    Paper.AddResult(d.ToString());
                    break;
                case Operation.Negate:
                    d = Convert.ToDouble(LastValue) * (-1.0F);
                    break;
                }
            }
            catch {
                d = 0;
                Window parent = (Window)MyPanel.Parent;
                Paper.AddResult("Error");
                MessageBox.Show(parent, "Operation cannot be perfomed", parent.Title);
            }

            return d;
        }
        private void CheckResult(double d)
        {
            if (Double.IsNegativeInfinity(d) || Double.IsPositiveInfinity(d) || Double.IsNaN(d))
                throw new Exception("Illegal value");
        }

        private void DisplayMemory()
        {
            if (_mem_val != String.Empty)
                BMemBox.Text = "Memory: " + _mem_val;
            else
                BMemBox.Text = "Memory: [empty]";
        }
        private void CalcResults()
        {
            double d;
            if (LastOper == Operation.None)
                return;

            d = Calc(LastOper);

            if (sBase =="HEX")
            {
                Int64 temp = Convert.ToInt64(d);
                Display = String.Format("{0:X}", temp);
                //Display = d.ToString();
            }
            else
            {
                Display = d.ToString();
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (Display == String.Empty)
                DisplayBox.Text = "0";
            else
                DisplayBox.Text = Display;
                ExpTop = String.Empty;
                UpdateDisplayExp();
        }
        private void UpdateDisplayExp()
        {
            if (ExpTop == String.Empty || ExpTop == null)
            {
                ExpBoxTop.Text = "";
                ExpBoxBot.Text = "";
            }
            else
            {
                ExpBoxTop.Text = ExpTop;
                ExpBoxBot.Text = "x10";
            }
        }
        private void AddToDisplay(char c)
        {bool update = false;

        if (Display == null)
        {
            update = true;
        }
        else
        {
        if (Display.Length <= 9)
                {
                    update = true;
                }

        }
                        
            if (update)
            {
                if (c == '.')
                {
                    if (Display.IndexOf('.', 0) >= 0)  //already exists
                        return;
                    Display = Display + c;
                }
                if (c >= 'A' && c <= 'F')
                {
                    Display = Display + c;
                }
                else
                {
                    if (c >= '0' && c <= '9')
                    {
                        Display = Display + c;
                    }
                    else
                        if (c == '\b')  //backspace ?
                        {
                            if (Display.Length <= 1)
                                Display = String.Empty;
                            else
                            {
                                int i = Display.Length;
                                Display = Display.Remove(i - 1, 1);  //remove last char 
                            }
                        }

                }

                UpdateDisplay();
            }
        }
        private void AddToDisplayExp(char c)
        {
            bool update = false;

        if (ExpTop == null)
        {
            update = true;
        }
        else
        {
        if (ExpTop.Length <= 2)
                {
                    update = true;
                }

        }

        if (update)
        {


            if (c >= '0' && c <= '9')
            {
                ExpTop = ExpTop + c;
            }
            else
                if (c == '\b')  //backspace ?
                {
                    if (ExpTop.Length <= 1)
                        ExpTop = String.Empty;
                    else
                    {
                        int i = ExpTop.Length;
                        ExpTop = ExpTop.Remove(i - 1, 1);  //remove last char 
                    }
                }

            UpdateDisplayExp();
        }
        }
   
        void OnMenuAbout(object sender, RoutedEventArgs e)
        {
            Window parent = (Window)MyPanel.Parent;
            MessageBox.Show(parent, parent.Title + " - By Andy Aberdeen ", parent.Title,MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void OnMenuExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void OnMenuStandard(object sender, RoutedEventArgs e)
        {
            //((MenuItem)ScientificMenu).IsChecked = false;
            ((MenuItem)StandardMenu).IsChecked = true; //for now always Standard
        }
        //Not implemenetd 
        void OnMenuScientific(object sender, RoutedEventArgs e)
        {
           //((MenuItem)StandardMenu).IsChecked = false; 
        }   
        private class PaperTrail
        {
            string args;

            public PaperTrail()
            {
            }
            public void AddArguments(string a)
            {
                args = a;
            }
            public void AddResult(string r)
            {
                PaperBox.Text += args + " = " + r + "\n";
            }
            public void Clear()
            {
                PaperBox.Text = string.Empty;
                args = string.Empty;
            }
        }


     }


}