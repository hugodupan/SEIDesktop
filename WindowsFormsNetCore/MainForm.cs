using Microsoft.Extensions.Options;
using SEI.Desktop.Models;
using SEI.Desktop.PageObjects;
using SEI.Desktop.Services;
using System;
using System.Windows.Forms;

namespace SEI.Desktop
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISampleService _sampleService;
        private readonly AppSettings _settings;

        public MainForm(IServiceProvider serviceProvider, ISampleService sampleService, IOptions<AppSettings> settings)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _sampleService = sampleService;
            _settings = settings.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxMatricula.Text))
            {
                MessageBox.Show("Matricula é obrigatória!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(comboBoxMarcador.Text))
            {
                MessageBox.Show("Marcador é obrigatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxQuantidade.Text))
            {
                MessageBox.Show("Quantidade é obrigatória!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int.Parse(textBoxQuantidade.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Quantidade é informada inválida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button1.Enabled = false;

            var matricula = textBoxMatricula.Text.Trim().ToLower();
            var marcador = comboBoxMarcador.Text.Trim().ToLower();
            var quantidade = textBoxQuantidade.Text.Trim().ToLower();

            progressBar1.Maximum = int.Parse(quantidade) + 1;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;


            try
            {
                DistribuirProcessos(matricula, marcador, quantidade);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro. Tente novamente. Erro:" + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                button1.Enabled = true;

            }
        }

        private void DistribuirProcessos(string matricula, string marcador, string quantidade)
        {

            var paginaSEI = new PaginaSEI(_settings);
            var marcadorASerEnviado = "roxo";
            progressBar1.Value = 1;


            try
            {
                paginaSEI.CarregarPaginaInicial();

                paginaSEI.EfetuarLogin();

                paginaSEI.FecharPopUp();

                for (int i = 1; i <= int.Parse(quantidade); i++)
                {
                    paginaSEI.VerPorMarcadores();

                    var qtdProcessosExistentes = paginaSEI.DetalharProcessosPorMarcador(marcador);

                    paginaSEI.IrParaUltimaPagina(qtdProcessosExistentes);

                    paginaSEI.DetalharUltimoProcesso();

                    paginaSEI.Autenticar();

                    paginaSEI.Credenciar(matricula);

                    if (!marcador.Contains("amarelo"))
                    {
                        paginaSEI.EnviarParaMarcador(marcadorASerEnviado);
                    }

                    paginaSEI.Descredenciar();

                    paginaSEI.IrParaControleProcessos();

                    progressBar1.Value += 1;


                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                paginaSEI.Fechar();
            }
        }
    }
}
