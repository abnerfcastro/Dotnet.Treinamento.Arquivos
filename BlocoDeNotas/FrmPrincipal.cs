using System;
using System.IO;
using System.Windows.Forms;

namespace BlocoDeNotas
{
    public partial class FrmPrincipal : Form
    {
        private string nomeArquivoAberto = null;

        public FrmPrincipal()
        {
            InitializeComponent();
            this.Text = "Bloco de Notas - Sem título";
        }

        /// <summary>
        /// Click Handler do botão 'Salvar Como'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvarComo_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Arquivos de Texto (*.txt) | *.txt";

            sfd.InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments);

            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var arquivo = sfd.OpenFile())
                    {
                        nomeArquivoAberto = sfd.FileName;
                        btnSalvar.Enabled = true;
                        this.Text = $"Bloco de Notas - {nomeArquivoAberto}";

                        using (StreamWriter sw = new StreamWriter(arquivo))
                        {
                            sw.Write(txtConteudo.Text);
                        }
                    }

                    MessageBox.Show(
                        "O arquivo foi salvo com sucesso!",
                        "Arquivo Salvo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        /// <summary>
        /// Click Handler do botão Abrir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            ofd.InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments);

            ofd.FileName = "";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var arquivo = ofd.OpenFile())
                    {
                        nomeArquivoAberto = ofd.FileName;
                        btnSalvar.Enabled = true;
                        this.Text = $"Bloco de Notas - {nomeArquivoAberto}";

                        using (StreamReader sr = new StreamReader(arquivo))
                        {
                            txtConteudo.Text = sr.ReadToEnd();
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(
                        exception.Message,
                        "Erro ao abrir o arquivo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Click Handler para o botão Salvar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (nomeArquivoAberto == null)
                return;

            try
            {
                using (var arquivo = File.Open(nomeArquivoAberto, FileMode.Truncate))
                {
                    using (StreamWriter sw = new StreamWriter(arquivo))
                    {
                        sw.Write(txtConteudo.Text);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    exception.Message,
                    "Erro ao salvar o arquivo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}