using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EscolaBLL;
using EscolaDTO;

namespace Escola
{
    public partial class cadastro_Alunos : Form
    {
        int matAlunoSelecionado = -1;
        DateTime dataAtual = System.DateTime.Now;
        public cadastro_Alunos()
        {
            InitializeComponent();
            CenterToParent();
        }

        private void cadastro_Alunos_Load(object sender, EventArgs e)
        {
            carregaGrid();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*O código irá preencher os textbox conforme seleção no datagrid*/
            /*Linha atual que estiver selecionada aparecerá nos campos(textbox) acima do datagrid
             */
            int sel = dataGridView1.CurrentRow.Index;
            //Valor de cada datagrid será enviado sao seu respectivo textbox
            matAlunoSelecionado = Convert.ToInt32(dataGridView1["matricula", sel].Value);
            txtNome.Text = Convert.ToString(dataGridView1["nome", sel].Value);
            txtCadastro.Text = Convert.ToString(dataGridView1["cadastro", sel].Value);
            txtIdade.Text = Convert.ToString(dataGridView1["idade", sel].Value);
            txtEndereco.Text = Convert.ToString(dataGridView1["endereco", sel].Value);
            txtMatricula.Text = Convert.ToString(dataGridView1["matricula", sel].Value);
            txtTelefone.Text = Convert.ToString(dataGridView1["telefone", sel].Value);
            txtNomeMae.Text = Convert.ToString(dataGridView1["nome_mae", sel].Value);
            txtNomePai.Text = Convert.ToString(dataGridView1["nome_pai", sel].Value);
            rtbComentarios.Text = Convert.ToString(dataGridView1["comentarios", sel].Value);
            cboSexo.Text = Convert.ToString(dataGridView1["sexo", sel].Value);
            

        }

        public void carregaGrid()
        {
            try
            {
                IList<Aluno_DTO> listAlunosDTO = new List<Aluno_DTO>();
                listAlunosDTO = new Aluno_BLL().cargaAluno();

                
                

                //Preenche dados no DataGridView
                dataGridView1.DataSource = listAlunosDTO;

                //Oculta as colunas
                dataGridView1.Columns[6].Visible = false; //primeira_nota
                dataGridView1.Columns[7].Visible = false; //segunda_nota
                dataGridView1.Columns[8].Visible = false; //terceira_nota
                dataGridView1.Columns[9].Visible = false; //quarta_nota
                dataGridView1.Columns[10].Visible = false; //media
                dataGridView1.Columns[11].Visible = false; //situacao

                //Nome das colunas
                dataGridView1.Columns["matricula"].HeaderText = "Matrícula";
                dataGridView1.Columns["nome"].HeaderText = "Nome";
                dataGridView1.Columns["idade"].HeaderText = "Idade";
                dataGridView1.Columns["endereco"].HeaderText = "Endereço"; // Ou através do índice: dataGridView1.Columns[3].HeaderText = "Endereço";
                dataGridView1.Columns["sexo"].HeaderText = "Sexo";
                dataGridView1.Columns["telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["comentarios"].HeaderText = "Comentários";
                dataGridView1.Columns["cadastro"].HeaderText = "Cadastro";
                dataGridView1.Columns["nome_mae"].HeaderText = "Nome da Mãe";
                dataGridView1.Columns["nome_pai"].HeaderText = "Nome do Pai";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void limpaCampos()
        {
            txtNome.Text = "";
            txtIdade.Text = "";
            txtEndereco.Text = "";
            txtMatricula.Text = "";
            txtTelefone.Text = "";
            rtbComentarios.Text = "";
            cboSexo.Text = "";
            txtNomeMae.Text = "";
            txtNomePai.Text = "";
            txtNome.Focus();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            
            
            if (DialogResult.Yes == MessageBox.Show("Deseja cadastrar esse aluno?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    //objeto ALU
                    Aluno_DTO ALU = new Aluno_DTO();
                    ALU.nome = txtNome.Text;
                    ALU.idade = Convert.ToInt32(txtIdade.Text);
                    ALU.primeira_nota = 0;
                    ALU.segunda_nota = 0;
                    ALU.terceira_nota = 0;
                    ALU.quarta_nota = 0;
                    ALU.media = 0;
                    ALU.situacao = "";
                    ALU.telefone = txtIdade.Text;
                    ALU.cadastro = dataAtual;
                    ALU.endereco = txtEndereco.Text;
                    ALU.nome_mae = txtNomeMae.Text;
                    ALU.nome_pai = txtNomePai.Text;
                    ALU.comentarios = rtbComentarios.Text;
                    switch (cboSexo.Text)
                    {
                        case "Masculino":
                            ALU.sexo = cboSexo.Text;break;
                        case "Feminino":
                            ALU.sexo = cboSexo.Text;break;
                    }
                    //Método insereAluno() na classe Aluno_BLL
                    int x = new Aluno_BLL().insereAluno(ALU);
                    if (x > 0)
                    {
                        MessageBox.Show("Aluno registrado com sucesso!","Mensagem", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    carregaGrid();
                }
                catch (System.FormatException ex)
                {
                    MessageBox.Show($"Campo inválido '{ex}' ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (matAlunoSelecionado < 0)
            {
                MessageBox.Show("Selecione um aluno antes!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Deseja editar esse aluno?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    
                    //objeto ALU
                    Aluno_DTO ALU = new Aluno_DTO();
                    ALU.matricula = matAlunoSelecionado;
                    ALU.nome = txtNome.Text;
                    ALU.idade = Convert.ToInt32(txtIdade.Text);
                    ALU.telefone = txtIdade.Text;
                    ALU.cadastro = dataAtual;
                    ALU.endereco = txtEndereco.Text;
                    ALU.nome_mae = txtNomeMae.Text;
                    ALU.nome_pai = txtNomePai.Text;
                    ALU.comentarios = rtbComentarios.Text;
                    switch (cboSexo.Text)
                    {
                        case "Masculino":
                            ALU.sexo = cboSexo.Text; break;
                        case "Feminino":
                            ALU.sexo = cboSexo.Text; break;
                    }
                    // Método editaAluno() na classe Aluno_BLL
                    int x = new Aluno_BLL().editaAluno(ALU);
                    //Verifica se houve gravação
                    if (x > 0)
                    {
                        MessageBox.Show("Aluno editado com sucesso!", "Editado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid();
                    }
                    carregaGrid();

                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (matAlunoSelecionado < 0)
            {
                MessageBox.Show("Selecione um aluno antes!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("Deseja apagar esse registro?", "Confirmação",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    //objeto ALU
                    Aluno_DTO ALU = new Aluno_DTO();
                    ALU.matricula = matAlunoSelecionado;
                    int x = new Aluno_BLL().deletaAluno(ALU);
                    if (x > 0)
                    {
                        MessageBox.Show("Registro apagado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid();
                    }
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }
    }

}
