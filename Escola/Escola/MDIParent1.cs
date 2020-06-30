using System;
using System.Windows.Forms;

namespace Escola
{
    public partial class MDIParent1 : Form
    {
        
        public MDIParent1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void alunoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new cadastro_Alunos();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void professorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Form childForm = new registro_Professor();
            childForm.MdiParent = this;
            childForm.Show();*/
        }

        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new registro_Notas();
            childForm.MdiParent = this;
            childForm.Show();
        }
    }
}

