using FluentValidation.Results;
using GerenciadorTeste.Dominio.ModuloDisciplina;
using GerenciadorTeste.WinApp;
using GerenciadorTestes.WinApp;
using System;
using System.Windows.Forms;

namespace GerenciadorTeste.WinForms.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinaForm : Form
    {
        public TelaCadastroDisciplinaForm()
        {
            InitializeComponent();
        }
        private Disciplina disciplina;

        public Func<Disciplina, ValidationResult> GravarRegistro { get; set; }

        public Disciplina Disciplina
        {

            get { return disciplina; }
            set
            {
                disciplina = value;

                txtId.Text = disciplina.Id.ToString();
                txtDisciplina.Text = disciplina.Nome;

            }
        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            txtDisciplina.Text = disciplina.Nome;


            var resultadoValidacao = GravarRegistro(disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
        private void TelaCadastroDisciplinaForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void TelaCadastroDisciplinaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
