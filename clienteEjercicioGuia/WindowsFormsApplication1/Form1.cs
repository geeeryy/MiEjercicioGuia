﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Longitud.Checked)
            {
                string mensaje = "1/" + nombre.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split ('\0')[0];
                MessageBox.Show("La longitud de tu nombre es: " + mensaje);
            }
            else if (Bonito.Checked)
            {
                string mensaje = "2/" + nombre.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                if (mensaje == "SI")
                    MessageBox.Show("Tu nombre ES bonito.");
                else
                    MessageBox.Show("Tu nombre NO bonito. Lo siento.");
            }
            else
            {
                string mensaje = "3/" + nombre.Text + "/" + alturaBox.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
