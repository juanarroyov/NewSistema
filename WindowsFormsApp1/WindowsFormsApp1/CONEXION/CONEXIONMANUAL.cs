using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NewSistema.CONEXION
{
    public partial class CONEXIONMANUAL : Form
    {
        private LIBRERIAS.AES aes = new LIBRERIAS.AES();
        public CONEXIONMANUAL()
        {
            InitializeComponent();
        }
        public void SavetoXml(Object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
        string dbcnString;
        public void ReadfromXML()
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load("ConnectionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                txtCnString.Text = (aes.Decrypt(dbcnString, LIBRERIAS.Desencryptacion.appPwdUnique, 
                    int.Parse("256")));
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {

            }
        }
        private void CONEXIONMANUAL_Load(object sender, EventArgs e)
        {
            ReadfromXML();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SavetoXml(aes.Encrypt(txtCnString.Text, LIBRERIAS.Desencryptacion.appPwdUnique, 
                int.Parse("256")));
        }
    }
}
