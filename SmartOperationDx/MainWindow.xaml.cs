using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Paragraph = System.Windows.Documents.Paragraph;

namespace SmartOperationDx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private int _counter = 0;
        private readonly string[] aiMessages =
        {
            "Warning, if predictions are accurate a limited shutdown of production will occur in approximately 3 hours",
            "It seems like the issue might be stemming from a miss configuration of a PID tag [tag name]",
            "Calculations estimate that the PID tag should be recalibrated to \nP:x \nI:x \nD:x ",
            "Sure, I have adjusted the prediction widgets to show the predictions for the system if the PID tag [tag name] was recalibrated"
        };

        private Line predictionLine;

        public MainWindow()
        {
            InitializeComponent();
            DrawGraph();
            DrawAxes();
            DrawUnits();
            AddLegend();
            DrawGridLines();
            IssueWarning();
            CreateAlarms();
        }

        private void CreateAlarms()
        {
            List<Alarm> alarms = new List<Alarm>
            {
                new Alarm("Pump_01", "H", "High", "Pump_01 Overheating", "85%"),
                new Alarm("Tank_02", "H", "High", "Tank_02 High Level", "47%"),
                new Alarm("Compressor_01", "HH", "Critical", "Compressor_01 Failure", "92%"),
                new Alarm("Boiler_01", "H", "High", "Boiler_01 Pressure High", "63%"),
                new Alarm("Generator_01", "HH", "Critical", "Generator_01 Overload", "78%"),
                new Alarm("Reactor_02", "H", "High", "Reactor_02 Temperature High", "54%"),
                new Alarm("Conveyor_03", "HH", "Critical", "Conveyor_03 Jammed", "89%"),
                new Alarm("Chiller_01", "H", "High", "Chiller_01 Low Coolant", "35%"),
                new Alarm("Fan_04", "L", "High", "Fan_04 Speed Low", "22%"),
                new Alarm("Heater_02", "HH", "Critical", "Heater_02 Overheating", "95%"),
                new Alarm("Mixer_01", "H", "High", "Mixer_01 Motor Fault", "67%"),
                new Alarm("Filter_03", "HH", "Critical", "Filter_03 Blocked", "74%"),
                new Alarm("Blower_02", "L", "High", "Blower_02 Pressure Low", "41%"),
                new Alarm("Dryer_01", "HH", "Critical", "Dryer_01 Overload", "88%"),
                new Alarm("Press_02", "H", "High", "Press_02 Hydraulic Failure", "59%")
            };
            AlarmTable.ItemsSource = alarms;
        }

        private void UpdateAlarms()
        {
            List<Alarm> alarms = new List<Alarm>
            {
                new Alarm("Pump_01", "H", "High", "Pump_01 Overheating", "85%"),
                new Alarm("Generator_01", "HH", "Critical", "Generator_01 Overload", "78%"),
                new Alarm("Conveyor_03", "HH", "Critical", "Conveyor_03 Jammed", "59%"),
                new Alarm("Fan_04", "L", "High", "Fan_04 Speed Low", "22%"),
                new Alarm("Mixer_01", "H", "High", "Mixer_01 Motor Fault", "37%"),
                new Alarm("Dryer_01", "HH", "Critical", "Dryer_01 Overload", "26%"),
            };
            AlarmTable.ItemsSource = alarms;
        }

        private async void IssueWarning()
        {
            await Task.Delay(10000);

            AppendMessage("", aiMessages[0], Brushes.Orange, TextAlignment.Right);
            _counter++;
        }

        private void UserChatInput_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Trigger your event or method here
                OnEnterPressed();
                e.Handled = true;
            }
        }

        private void DrawAxes()
        {
            // X-axis
            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = GraphCanvas.Height,
                X2 = GraphCanvas.Width,
                Y2 = GraphCanvas.Height,
                Stroke = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                StrokeThickness = 1
            };

            // Y-axis
            Line yAxis = new Line
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = GraphCanvas.Height,
                Stroke = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                StrokeThickness = 1
            };

            GraphCanvas.Children.Add(xAxis);
            GraphCanvas.Children.Add(yAxis);
        }
        private void DrawUnits()
        {
            int unitSize = 20; // Size of each unit in pixels
            int numberOfUnitsX = (int)(GraphCanvas.Width / unitSize);
            int numberOfUnitsY = (int)(GraphCanvas.Height / unitSize);

            // Draw units on X-axis
            for (int i = 0; i <= numberOfUnitsX; i += 5)
            {
                TextBlock unitLabel = new()
                {
                    Text = $"{i.ToString()}m" ,
                    Foreground = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                    FontSize = 10,
                    RenderTransform = new TranslateTransform(i * unitSize - 5, GraphCanvas.Height + 5)
                };

                GraphCanvas.Children.Add(unitLabel);
            }

            // Draw units on Y-axis
            for (int i = 0; i <= numberOfUnitsY; i += 5)
            {

                TextBlock unitLabel = new()
                {
                    Text = $"{i.ToString()}kg",
                    Foreground = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                    FontSize = 10,
                    RenderTransform = new TranslateTransform(-30, GraphCanvas.Height - i * unitSize - 5)
                };

                GraphCanvas.Children.Add(unitLabel);
            }
        }
        private void DrawGridLines()
        {
            int unitSize = 60; // Size of each unit in pixels
            int numberOfUnitsX = (int)(GraphCanvas.Width / unitSize);
            int numberOfUnitsY = (int)(GraphCanvas.Height / unitSize);

            // Draw vertical grid lines
            for (int i = 1; i < numberOfUnitsX; i++)
            {
                Line gridLine = new Line
                {
                    X1 = i * unitSize,
                    Y1 = 0,
                    X2 = i * unitSize,
                    Y2 = GraphCanvas.Height,
                    Opacity = 0.3,
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 1
                };

                GraphCanvas.Children.Add(gridLine);
            }

            // Draw horizontal grid lines
            for (int i = 1; i < numberOfUnitsY; i++)
            {
                Line gridLine = new Line
                {
                    X1 = 0,
                    Y1 = i * unitSize,
                    X2 = GraphCanvas.Width - 80,
                    Y2 = i * unitSize,
                    Opacity = 0.3,
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 1
                };

                GraphCanvas.Children.Add(gridLine);
            }
        }
        private void DrawGraph()
        {
            // Clear previous drawings
            //GraphCanvas.Children.Clear();

            Line targetLine = new Line
            {
                X1 = 3,
                Y1 = GraphCanvas.Height - 3,
                X2 = GraphCanvas.Width - 80,
                Y2 = 3,
                Stroke = Brushes.Red,
                StrokeDashArray = new DoubleCollection() { 4, 2 },
                StrokeThickness = 2
            };

            GraphCanvas.Children.Add(targetLine);

            predictionLine = new Line
            {
                X1 = 3,
                Y1 = GraphCanvas.Height - 3,
                X2 = GraphCanvas.Width - 80,
                Y2 = 50,
                Stroke = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                StrokeThickness = 2
            };

            GraphCanvas.Children.Add(predictionLine);
        }

        private void DrawUpdatedGraph()
        {
            //GraphCanvas.Children.Remove(predictionLine);

            Line updatedPrediction = new Line
            {
                X1 = 3,
                Y1 = GraphCanvas.Height - 3,
                X2 = GraphCanvas.Width - 80,
                Y2 = 25,
                Stroke = Brushes.DeepSkyBlue,
                StrokeThickness = 2
            };

            GraphCanvas.Children.Add(updatedPrediction);

            // Create a legend for the diagonal line
            TextBlock legendLabel = new TextBlock
            {
                Text = "New Prediction",
                FontSize = 12,
                Foreground = Brushes.DeepSkyBlue,
                RenderTransform = new TranslateTransform(GraphCanvas.Width - 10, 50)
            };

            // Add a sample line to the legend
            Line legendLine = new Line
            {
                X1 = GraphCanvas.Width - 20,
                Y1 = 60,
                X2 = GraphCanvas.Width - 40,
                Y2 = 60,
                Stroke = Brushes.DeepSkyBlue,
                StrokeThickness = 2,
            };

            GraphCanvas.Children.Add(legendLabel);
            GraphCanvas.Children.Add(legendLine);
        }

        private void AddLegend()
        {
            // Create a legend for the diagonal line
            TextBlock legendLabel = new TextBlock
            {
                Text = "Target",
                FontSize = 12,
                Foreground = Brushes.Red,
                RenderTransform = new TranslateTransform(GraphCanvas.Width - 10, 10)
            };

            // Add a sample line to the legend
            Line legendLine = new Line
            {
                X1 = GraphCanvas.Width - 20,
                Y1 = 20,
                X2 = GraphCanvas.Width - 40,
                Y2 = 20,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection() { 4, 2 } // Dash pattern: 4 units on, 2 units off
            };

            GraphCanvas.Children.Add(legendLabel);
            GraphCanvas.Children.Add(legendLine);

            // Create a legend for the diagonal line
            TextBlock legendLabel2 = new TextBlock
            {
                Text = "Prediction",
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                RenderTransform = new TranslateTransform(GraphCanvas.Width - 10, 30)
            };

            // Add a sample line to the legend
            Line legendLine2 = new Line
            {
                X1 = GraphCanvas.Width - 20,
                Y1 = 40,
                X2 = GraphCanvas.Width - 40,
                Y2 = 40,
                Stroke = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                StrokeThickness = 2,
            };

            GraphCanvas.Children.Add(legendLabel2);
            GraphCanvas.Children.Add(legendLine2);
        }

        private void OnEnterPressed()
        {
            // Create a TextRange covering the entire content of the RichTextBox
            TextRange textRange = new(UserChatInput.Document.ContentStart, UserChatInput.Document.ContentEnd);

            // Get the text content as a string
            string richTextBoxContent = textRange.Text;
            AppendMessage("", richTextBoxContent, Brushes.SeaShell, TextAlignment.Left);
            UserChatInput.Document.Blocks.Clear();
            UserChatInput.CaretPosition = UserChatInput.Document.ContentStart;
            AiResponse();
        }

        private async void AiResponse()
        {
            var responseIndicator = AppendMessage("", "Responding...", Brushes.Aqua, TextAlignment.Right);
            await Task.Delay(2000);
            AssistantChatWindow.Document.Blocks.Remove(responseIndicator);
            AppendMessage("", GetAiResponse(), Brushes.Aqua, TextAlignment.Right);
            _counter++;
        }

        private string GetAiResponse()
        {
            if (_counter >= aiMessages.Length)
            {
                return "";
            }

            if (_counter == 3)
            {
                UpdatePredictions();
                UpdateAlarms();
            }
            return aiMessages[_counter];
        }

        private void UpdatePredictions()
        {
            DrawUpdatedGraph();
            
        }

        private Paragraph AppendMessage(string sender, string message, Brush color, TextAlignment alignment)
        {
            Paragraph paragraph = new();
            paragraph.Inlines.Add(new Run(sender) { Foreground = color, FontWeight = FontWeights.Bold });
            paragraph.Inlines.Add(new Run(message) { Foreground = color });
            paragraph.TextAlignment = alignment;
            AssistantChatWindow.Document.Blocks.Add(paragraph);

            // Scroll to the end of the RichTextBox
            AssistantChatWindow.ScrollToEnd();

            return paragraph;
        }
    }
}
