using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using Microsoft.ML;


namespace FINAL_PROJPF
{
    public partial class Form1 : Form
    {
        bool _streaming;
        Capture _capture;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _streaming = false;
            _capture = new Capture();

        }
        private void streaming(object sender , EventArgs e)
        {
            var img = _capture.QueryFrame().ToImage<Bgr,byte> ();
            var bmp = img.Bitmap;
            picturePreview.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_streaming)
            {
                Application.Idle += streaming;
                button1.Text = @"Stop Streaming";
                button1.ForeColor = Color.White;
                button1.BackColor = Color.Red;
            }

            else { 
                Application.Idle -= streaming;
                button1.Text = @"Start Streaming";
                button1.ForeColor = Color.Black;
                button1.BackColor = Color.Gainsboro;
            }
            _streaming = !_streaming;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Create an instance of OpenFileDialog
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureCaptured.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureCaptured.Image = picturePreview.Image;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(pictureCaptured != null)
            {
                string[] names = { "CELLPHONE","MOUSE", "KEYBOARD" };
                string input = "sequential_1_input";
                string output = "sequential_3";
                var context = new MLContext();
                List<Input> List = new List<Input>();
                var data = context.Data.LoadFromEnumerable<Input>(List);
                var estimator = context.Transforms.ResizeImages(
                    input, 224, 224, nameof (Input.bitmap),
                    Microsoft.ML.Transforms.Image.ImageResizingEstimator.ResizingKind.Fill)
                    .Append(context.Transforms.ExtractPixels(input))
                    .Append(context.Transforms.ApplyOnnxModel(output, input, "Gadget.onnx"));
                var model = estimator.Fit (data);
                Bitmap bmp = new Bitmap(224, 224);
                Graphics.FromImage(bmp).DrawImage(pictureCaptured.Image, 0, 0, 224, 224);
                var predictionEngine = context.Model.CreatePredictionEngine<Input,Output>(model);
                var predict = predictionEngine.Predict(new Input { bitmap = bmp });
                label2.Text = names[Array.FindIndex(predict.labels, p => p == predict.labels.Max())];
            }

        }
       

        private void button4_Click(object sender, EventArgs e)
        {
            // Display a message box asking if the user wants to exit the program
            DialogResult result = MessageBox.Show("Are you sure you want to exit the program?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the result
            if (result == DialogResult.Yes)
            {
                // If the user clicked Yes, close the application
                this.Close();
            }
            // If the user clicked No, do nothing (the message box will close automatically)
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureCaptured_Click(object sender, EventArgs e)
        {

        }

        private void sidePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
