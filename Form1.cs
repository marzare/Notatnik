using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notatnik
{
    public partial class Form1 : Form
    {
        string linia;
        public Form1()
        {
            InitializeComponent();
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linia = string.Empty;
            textBox.Clear();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames= true, Multiselect= false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            linia = ofd.FileName;
                            Task<string> text = sr.ReadToEndAsync();
                            textBox.Text = text.Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Wiadomosc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(linia))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            linia = sfd.FileName;
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(linia);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Wiadomosc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(linia))
                    {
                        await sw.WriteLineAsync(linia);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Wiadomosc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(linia);
                        }
                    } catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Wiadomosc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
