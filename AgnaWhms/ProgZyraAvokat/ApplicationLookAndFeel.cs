using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;

namespace ProgZyraAvokat
{
    public class ApplicationLookAndFeel
    {
        public static void ApplyTheme(TextBox c, int txtSize, Color txtBoxBackColor, Color txtBoxForeColor)
        {
            try
            {
                //c.Font = new Font("Century Gothic", txtSize); c.BackColor = txtBoxBackColor; c.ForeColor = txtBoxForeColor;
                c.Font = new Font("Century Gothic", txtSize); c.BackColor = txtBoxBackColor; c.ForeColor = txtBoxForeColor;
                //c.BorderStyle = BorderStyle.None;  
                c.CharacterCasing = CharacterCasing.Upper;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ApplyTheme TextBox " + ex.Message);
            }

        }
        public static void ApplyTheme(Label c, int txtSize, Color txtBoxBackColor, Color txtBoxForeColor)
        {
            try
            {
                //c.Font = new Font("Century Gothic", txtSize); c.BackColor = txtBoxBackColor; c.ForeColor = txtBoxForeColor;
                c.Font = new Font("Century Gothic", txtSize); c.BackColor = txtBoxBackColor; c.ForeColor = txtBoxForeColor;
                c.BorderStyle = BorderStyle.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ApplyTheme Label " + ex.Message);
            }
        }
        public static void ApplyTheme(Form c, int txtSize, Color txtBoxBackColor, Color txtBoxForeColor)
        {
            try
            {
                //SystemFonts.MessageBoxFont;
                //c.Font = new Font("Century Gothic", txtSize);
                c.Font = new Font("Century Gothic", txtSize);
                //c.Font = SystemFonts.MessageBoxFont;// new Font(SystemFonts.MessageBoxFont.ToString() , txtSize);
                c.BackColor = txtBoxBackColor;
                c.ForeColor = txtBoxForeColor;
                c.ControlBox = false;
                //c.fon
                //c.BackColor = Color.LimeGreen;
                //c.TransparencyKey = Color.DodgerBlue ;  
            }
            catch (Exception ex)
            {
                MessageBox.Show("ApplyTheme Form " + ex.Message);
            }
        }
        public static void ApplyTheme(Button c, int txtSize, bool Bold, Color txtBoxBackColor, Color txtBoxForeColor)
        {
            try
            {
                c.Font = new Font("Century Gothic", txtSize);
                c.BackColor = txtBoxBackColor;
                c.ForeColor = txtBoxForeColor;
                c.UseVisualStyleBackColor = true;
                if (Bold) c.Font = new Font(c.Font, FontStyle.Bold);
                //c.bo
                {
                    //c.MouseEnter += new System.EventHandler(button1_MouseEnter);
                    //c.MouseLeave += new System.EventHandler(button1_MouseLeave);
                }
                //c.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ApplyTheme Button " + ex.Message);
            }
        }
        public static void button1_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).UseVisualStyleBackColor = false;
            ((Button)sender).BackColor = Color.GhostWhite;
        }
        public static void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).UseVisualStyleBackColor = true;
        }
        public static void ApplyTheme(DataGridView C, int txtSize, bool Bold, Color txtBoxBackColor, Color txtBoxForeColor)
        {
            try
            {
                //C.BackgroundColor = txtBoxBackColor;
                //C.ForeColor = txtBoxForeColor;
                //C.BorderStyle = BorderStyle.None;
                //// Set property values appropriate for read-only display and // limited interactivity. 
                //C.AllowUserToAddRows = false;
                //C.AllowUserToDeleteRows = false;
                //C.AllowUserToOrderColumns = true;
                //C.ReadOnly = true;
                ////C.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //C.MultiSelect = false;
                //C.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells ;
                //C.AllowUserToResizeColumns = false;
                //C.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                //C.AllowUserToResizeRows = false;
                //C.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                C.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                C.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                C.RowTemplate.Height = 50;
                C.ForeColor = txtBoxForeColor;
                C.BackgroundColor = txtBoxBackColor;
                C.AlternatingRowsDefaultCellStyle.BackColor = txtBoxBackColor;
                C.CellBorderStyle = DataGridViewCellBorderStyle.None;
                C.RowsDefaultCellStyle.BackColor = txtBoxBackColor;
                C.ReadOnly = true;
                //groupBox1.ForeColor = lblForeColor1;
                // Set the selection background color for all the cells.
                //C.DefaultCellStyle.SelectionBackColor = Color.White;
                //C.DefaultCellStyle.SelectionForeColor = Color.Black;
                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                //C.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
                // Set the background color for all rows and for alternating rows. 
                // The value for alternating rows overrides the value for all rows. 
                //C.RowsDefaultCellStyle.BackColor = Color.LightGray;
                //C.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                // Set the row and column header styles.
                //C.ColumnHeadersDefaultCellStyle.ForeColor = txtBoxForeColor;// Color.White;
                //C.ColumnHeadersDefaultCellStyle.BackColor = txtBoxBackColor;//Color.Black;
                //C.RowHeadersDefaultCellStyle.BackColor = Color.Black;
                // Set the Format property on the "Last Prepared" column to cause
                // the DateTime to be formatted as "Month, Year".
                //C.Columns["Last Prepared"].DefaultCellStyle.Format = "y";
                // Specify a larger font for the "Ratings" column. 
                //using (Font font = new Font(
                //    C.DefaultCellStyle.Font.FontFamily, 25, FontStyle.Bold))
                //{
                //    C.Columns["Rating"].DefaultCellStyle.Font = font;
                //}
                // Attach a handler to the CellFormatting event.
                //c.CellFormatting += new
                //DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ApplyTheme Button " + ex.Message);
            }
        }
        public static void UseTheme(Form form, int txtSize)//,Color formBackColor,Color formForeColor)
        {
            try
            {

                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                Global.screenHeight = screen.Height;
                Global.screenWidth = screen.Width;
                Color formBackColorAll = ColorTranslator.FromHtml("#22304d");//#424242
                Color lblForeColor1 = ColorTranslator.FromHtml("#F2F2F2");
                Color buttonForeColor1 = ColorTranslator.FromHtml("#F2F2F2");
                Color buttonBackColor = ColorTranslator.FromHtml("#FF5733");
                if (form.Text.ToUpper() == "FORMEKLIENTE"
                //|| form.Text.ToUpper() == "ROUTA" 
                || form.Text.ToUpper() == "LISTESHITJE"
                || form.Text.ToUpper() == "LOGIN" || form.Text.ToUpper() == "MAINMENU"
                || form.Text.ToUpper() == "LISTEBRANDE" || form.Text.ToUpper() == "LISTEKLIENTE")//form.Text.ToUpper() == "ARTIKUJ" || 

                {
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }
                else if (form.Text.ToUpper() == "RAPORTE")//form.Text.ToUpper() == "ARTIKUJ" || 
                {
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }
                else if (form.Text.ToUpper() == "FORMEDINAMIKE")//form.Text.ToUpper() == "ARTIKUJ" || 
                {
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }
                else
                {
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }

                foreach (var c in form.Controls)
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Windows.Forms.TextBox":
                            //ApplyTheme((TextBox)c, 18, Color.DodgerBlue, Color.White);
                            if (form.Text.ToUpper() == "ARTIKUJ")
                            {
                                //ApplyTheme((TextBox)c, txtSize, Color.DodgerBlue, Color.White);
                            }
                            else
                            {
                                ApplyTheme((TextBox)c, 14, formBackColorAll, lblForeColor1);
                            }
                            break;
                        case "System.Windows.Forms.Label":
                            if (form.Text.ToUpper() != "FORMEKLIENTE")
                            {
                                ApplyTheme((Label)c, 14, formBackColorAll, lblForeColor1);
                            }
                            //}

                            break;
                        case "System.Windows.Forms.Button":
                            if (form.Text.ToUpper() != "ARTIKUJ" && form.Text.ToUpper() != "FORMEKLIENTE"
                                && form.Text.ToUpper() != "ROUTA" && form.Text.ToUpper() != "LISTESHITJE"
                                && form.Text.ToUpper() != "RAPORTE" && form.Text.ToUpper() != "LOGIN"
                                && form.Text.ToUpper() != "LISTEBRANDE" && form.Text.ToUpper() != "MAINMENU"
                                && form.Text.ToUpper() != "LISTEKLIENTE" && form.Text.ToUpper() != "MJETETHEMELORE")
                            {
                                ApplyTheme((Button)c, txtSize, true, buttonBackColor, lblForeColor1);//Color.DodgerBlue, Color.White
                            }
                            break;
                        case "System.Windows.Forms.DataGridView":
                            //ApplyTheme((DataGridView)c, txtSize, true, formBackColorAll, lblForeColor1);
                            break;
                    }
                }

                //Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                //MessageBox.Show("screen.Width " + screen.Width.ToString() + ",screen.Height " + screen.Height.ToString() );
                int w = form.Width >= screen.Width ? screen.Width : (screen.Width + form.Width) / 2;
                int h = form.Height >= screen.Height ? screen.Height : (screen.Height + form.Height) / 2;
                if (form.Text.ToUpper() == "ARTIKUJ")
                {
                    form.Location = new Point(((screen.Width - w) / 2 - 100), (screen.Height - h) / 2);
                    form.Size = new Size(w, h + 200);
                }
                else if (form.Text.ToUpper() == "SHITESA RRUGEKALIME DITORE")
                {
                    form.Location = new Point(((screen.Width - w) / 2), ((screen.Height - h) / 2 - 50));
                    form.Size = new Size(w, h + 350);
                }
                else
                {
                    form.Location = new Point(((screen.Width - w) / 2 - 50), ((screen.Height - h) / 2 - 100));
                    form.Size = new Size(w, h + 300);
                }

                //form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim UseTheme " + ex.Message);
            }

        }
        public static void UseThemeSido(Form form, int txtSize,Color formBackColorAll,Color lblForeColor1,
           Color buttonForeColor1,Color buttonBackColor)//,Color formBackColor,Color formForeColor)
        {
            try
            {

                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                Global.screenHeight = screen.Height;
                Global.screenWidth = screen.Width;
                ////formBackColorAll = ColorTranslator.FromHtml("#424242");
                ////lblForeColor1 = ColorTranslator.FromHtml("#F2F2F2");
                ////buttonForeColor1 = ColorTranslator.FromHtml("#F2F2F2");
                ////buttonBackColor = ColorTranslator.FromHtml("#FF5733");
                if (form.Text.ToUpper() == "FORMEKLIENTE"
                //|| form.Text.ToUpper() == "ROUTA" 
                || form.Text.ToUpper() == "LISTESHITJE"
                || form.Text.ToUpper() == "LOGIN" || form.Text.ToUpper() == "MAINMENU"
                || form.Text.ToUpper() == "LISTEBRANDE" || form.Text.ToUpper() == "LISTEKLIENTE")//form.Text.ToUpper() == "ARTIKUJ" || 

                {
                    //Color formBackColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color formBackColor = ColorTranslator.FromHtml("#424242");//#800000//red and black//#ffffff/White//#00ffff//Light Blue
                    //Color formForeColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color lblBackColor1 = ColorTranslator.FromHtml("#F2F2F2"); //Color.White; ColorTranslator.FromHtml("#F8F8FF");///Color.FromArgb(234, 101, 148);// /234, 101, 148//#22B573
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }
                else if (form.Text.ToUpper() == "RAPORTE")//form.Text.ToUpper() == "ARTIKUJ" || 
                {
                    //Color formBackColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color formBackColor = ColorTranslator.FromHtml("#424242");//#800000//red and black//#ffffff/White//#00ffff//Light Blue
                    //Color formForeColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color lblBackColor1 = ColorTranslator.FromHtml("#F2F2F2"); //Color.White; ColorTranslator.FromHtml("#F8F8FF");///Color.FromArgb(234, 101, 148);// /234, 101, 148//#22B573
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);

                    //Color formBackColor = Color.White;// ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    ////Color formForeColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color lblBackColor1 = Color.White; ColorTranslator.FromHtml("#F2F2F2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //ApplyTheme(form, txtSize, formBackColorAll, lblBackColor1);
                }
                else if (form.Text.ToUpper() == "FORMEDINAMIKE")//form.Text.ToUpper() == "ARTIKUJ" || 
                {
                    //Color formBackColor = ColorTranslator.FromHtml("#424242");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color formForeColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color lblBackColor1 = ColorTranslator.FromHtml("#F2F2F2"); //Color.White;
                    //ColorTranslator.FromHtml("#FFFFFF");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }
                else
                {
                    //ApplyTheme(form, txtSize, Color.DodgerBlue, Color.LightSeaGreen);

                    //Color formBackColor = ColorTranslator.FromHtml("#424242");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color formForeColor = ColorTranslator.FromHtml("#29ABE2");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    //Color lblBackColor1 = ColorTranslator.FromHtml("#F2F2F2"); //Color.White;
                    //ColorTranslator.FromHtml("#FFFFFF");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                    ApplyTheme(form, txtSize, formBackColorAll, lblForeColor1);
                }

                //ApplyTheme(form, txtSize, Color.LightSlateGray, Color.LightSeaGreen );
                foreach (var c in form.Controls)
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Windows.Forms.TextBox":
                            //ApplyTheme((TextBox)c, 18, Color.DodgerBlue, Color.White);
                            if (form.Text.ToUpper() == "ARTIKUJ")
                            {
                                //ApplyTheme((TextBox)c, txtSize, Color.DodgerBlue, Color.White);
                            }
                            else
                            {
                                ApplyTheme((TextBox)c, 14, formBackColorAll, lblForeColor1);
                            }
                            break;
                        case "System.Windows.Forms.Label":
                            //if (form.Text.ToUpper() != "KATALOGE")// && form.Text.ToUpper() != "FORMEKLIENTE"
                            //&& form.Text.ToUpper() != "ROUTA" && form.Text.ToUpper() != "LISTESHITJE"
                            //&& form.Text.ToUpper() != "RAPORTE" && form.Text.ToUpper() != "LOGIN"
                            //&& form.Text.ToUpper() != "LISTEKLIENTE" && form.Text.ToUpper() != "LISTEBRANDE")
                            //{
                            if (form.Text.ToUpper() != "FORMEKLIENTE")
                            {
                                ApplyTheme((Label)c, 14, formBackColorAll, lblForeColor1);
                            }
                            //}

                            break;
                        case "System.Windows.Forms.Button":
                            if (form.Text.ToUpper() != "ARTIKUJ" && form.Text.ToUpper() != "FORMEKLIENTE"
                                && form.Text.ToUpper() != "ROUTA" && form.Text.ToUpper() != "LISTESHITJE"
                                && form.Text.ToUpper() != "RAPORTE" && form.Text.ToUpper() != "LOGIN"
                                && form.Text.ToUpper() != "LISTEBRANDE" && form.Text.ToUpper() != "MAINMENU"
                                && form.Text.ToUpper() != "LISTEKLIENTE" && form.Text.ToUpper() != "MJETETHEMELORE")
                            {
                                ApplyTheme((Button)c, txtSize, true, buttonBackColor, buttonForeColor1);//Color.DodgerBlue, Color.White
                            }
                            break;
                        case "System.Windows.Forms.DataGridView":
                            ApplyTheme((DataGridView)c, txtSize, true, formBackColorAll, lblForeColor1);
                            //DataGridView myGrid = (DataGridView)c;
                            //myGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            //myGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            //myGrid.RowTemplate.Height = 50;
                            break;
                    }
                }

                //Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                //MessageBox.Show("screen.Width " + screen.Width.ToString() + ",screen.Height " + screen.Height.ToString() );
                int w = form.Width >= screen.Width ? screen.Width : (screen.Width + form.Width) / 2;
                int h = form.Height >= screen.Height ? screen.Height : (screen.Height + form.Height) / 2;
                if (form.Text.ToUpper() == "ARTIKUJ")
                {
                    form.Location = new Point(((screen.Width - w) / 2 - 100), (screen.Height - h) / 2);
                    form.Size = new Size(w, h + 200);
                }
                else if (form.Text.ToUpper() == "SHITESA RRUGEKALIME DITORE")
                {
                    form.Location = new Point(((screen.Width - w) / 2), ((screen.Height - h) / 2 - 50));
                    form.Size = new Size(w, h + 350);
                }
                else
                {
                    form.Location = new Point(((screen.Width - w) / 2 - 50), ((screen.Height - h) / 2 - 100));
                    form.Size = new Size(w, h + 300);
                }

                //form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim UseTheme " + ex.Message);
            }

        }
        public static void UseTheme_Buttons(Form form, int txtSize)//,Color formBackColor,Color formForeColor)
        {
            try
            {

                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                Global.screenHeight = screen.Height;
                Global.screenWidth = screen.Width;
                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor1 = ColorTranslator.FromHtml("#F2F2F2");
                Color buttonForeColor1 = ColorTranslator.FromHtml("#F2F2F2");
                Color buttonBackColor = ColorTranslator.FromHtml("#FF5733");
                //ApplyTheme(form, txtSize, Color.LightSlateGray, Color.LightSeaGreen );
                foreach (var c in form.Controls)
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Windows.Forms.Button":
                            if (form.Text.ToUpper() != "ARTIKUJ" && form.Text.ToUpper() != "FORMEKLIENTE"
                                && form.Text.ToUpper() != "ROUTA" && form.Text.ToUpper() != "LISTESHITJE"
                                && form.Text.ToUpper() != "RAPORTE" && form.Text.ToUpper() != "LOGIN"
                                && form.Text.ToUpper() != "LISTEBRANDE" && form.Text.ToUpper() != "MAINMENU"
                                && form.Text.ToUpper() != "LISTEKLIENTE" && form.Text.ToUpper() != "MJETETHEMELORE")
                            {
                                ApplyTheme((Button)c, txtSize, true, buttonBackColor, lblForeColor1);//Color.DodgerBlue, Color.White
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim UseTheme " + ex.Message);
            }

        }
    }
}
