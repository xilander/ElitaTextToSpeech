using System;
using System.Speech;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

namespace ElitaTextToSpeech
{
    public partial class ElitaTTS : Form
    {
        public ElitaTTS()
        {
            InitializeComponent();
        }

        //SpeechSynthesizer Class Provides access to the functionality of an installed a speech synthesis engine.   
        SpeechSynthesizer speechSynthesizerObj;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            speechSynthesizerObj = new SpeechSynthesizer();
            btn_Resume.Enabled = false;
            btn_Pause.Enabled = false;
            labelFileName.Visible = false;
            textBoxFileName.Visible = false;
        }
        private void btn_Speak_Click(object sender, EventArgs e)
        {
            //Disposes the SpeechSynthesizer object   
            //speechSynthesizerObj.Dispose();
            try
            {
                if (richTextBox1.Text != "")
                {
                    speechSynthesizerObj = new SpeechSynthesizer();
                    //Asynchronously speaks the contents present in RichTextBox1
                    speechSynthesizerObj.SelectVoice("Microsoft Sabina Desktop");

                    if (checkBox1.Checked)
                    {
                        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        if (textBoxFileName.Text != "")
                        {
                            string fileName = textBoxFileName.Text;
                            filePath = filePath + "\\" + fileName + ".wav";
                            speechSynthesizerObj.SetOutputToWaveFile(filePath, new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
                            MessageBox.Show("Archivo wav generado, revise su escritorio", "Muy bien...");
                        }
                        else
                        {
                            MessageBox.Show("No ha ingresado el nombre del archivo", "Atención...");
                        }

                        speechSynthesizerObj.SpeakAsync(richTextBox1.Text);
                        btn_Pause.Enabled = true;
                        btn_Stop.Enabled = true;
                    }
                    else
                    {
                        speechSynthesizerObj.SpeakAsync(richTextBox1.Text);
                        btn_Pause.Enabled = true;
                        btn_Stop.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("No hay texto a sintetizar", "Atención...");
                }
            }
            catch (Exception ex)
            {
                long caseSwitch = Math.Abs(ex.HResult);

                switch (caseSwitch)
                {
                    case 2147024809:
                        MessageBox.Show(ex.Message, "Se ha producido un error !!!");
                        break;
                    case 2147024864:
                        MessageBox.Show(ex.Message + "\n \n Intente con otro nombre", "Se ha producido un error !!!");
                        break;
                    default:
                        MessageBox.Show(ex.Message, "Se ha producido un error !!!");
                        break;
                }
            }
        }
        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Gets the current speaking state of the SpeechSynthesizer object.   
                if (speechSynthesizerObj.State == SynthesizerState.Speaking)
                {
                    //Pauses the SpeechSynthesizer object.   
                    speechSynthesizerObj.Pause();
                    btn_Resume.Enabled = true;
                    btn_Speak.Enabled = false;
                }
            }
        }
        private void btn_Resume_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                if (speechSynthesizerObj.State == SynthesizerState.Paused)
                {
                    //Resumes the SpeechSynthesizer object after it has been paused.   
                    speechSynthesizerObj.Resume();
                    btn_Resume.Enabled = false;
                    btn_Speak.Enabled = true;
                }
            }
        }
        private void btn_Stop_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                labelFileName.Visible = true;
                textBoxFileName.Visible = true;
            }
            else
            {
                labelFileName.Visible = false;
                textBoxFileName.Visible = false;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
