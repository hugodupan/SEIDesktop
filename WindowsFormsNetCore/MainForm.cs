using Microsoft.Extensions.Options;
using SEI.Desktop.Models;
using SEI.Desktop.PageObjects;
using SEI.Desktop.Services;
using SEIDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SEI.Desktop
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISampleService _sampleService;
        private readonly AppSettings _settings;
        private List<Perito> _listaPeritos;
        private List<string> _listaMarcadores;

        public MainForm(IServiceProvider serviceProvider, ISampleService sampleService, IOptions<AppSettings> settings)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _sampleService = sampleService;
            _settings = settings.Value;

            _listaPeritos = new List<Perito>();
            _listaMarcadores = new List<string>();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBoxMatricula.Text))
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
                var quantidadeInt = int.Parse(textBoxQuantidade.Text);

                if (quantidadeInt <= 0)
                {
                    MessageBox.Show("Quantidade é informada inválida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Quantidade é informada inválida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var matricula = comboBoxMatricula.SelectedValue.ToString().Trim().ToLower();
            var nome = comboBoxMatricula.Text.ToString().Trim().ToLower();
            var marcador = comboBoxMarcador.Text.Trim().ToLower();
            var quantidade = textBoxQuantidade.Text.Trim().ToLower();

            InicializarComponentes(true, quantidade);

            try
            {
                DistribuirProcessos(matricula, nome, marcador, quantidade);
                MessageBox.Show("Processo finalizado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro. Tente novamente. Erro:" + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                InicializarComponentes(false, "0");
            }
        }

        private void InicializarComponentes(bool iniciar, string quantidade)
        {
            labelProgress.Text = "0/0";
            progressBar.Value = 0;
            progressBar.Maximum = int.Parse(quantidade);
            progressBar.Minimum = 0;

            if (iniciar)
            {
                botaoDistribuir.Enabled = false;
                labelProcessando.Visible = true;
            }
            else
            {
                botaoDistribuir.Enabled = true;
                labelProcessando.Visible = false;
            }

        }

        private void DistribuirProcessos(string matricula, string nome, string marcador, string quantidade)
        {

            var paginaSEI = new PaginaSEI(_settings, checkBoxVerNavegador.Checked);
            var marcadorASerEnviado = "roxo";
            labelProgress.Text = progressBar.Value + "/" + quantidade;

            var contadorDeProcessosPassados = 0;

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

                    contadorDeProcessosPassados++;

                    labelProgress.Text = i + "/" + quantidade;
                    progressBar.Value = i;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                textBoxQuantidade.Text = (int.Parse(textBoxQuantidade.Text) - contadorDeProcessosPassados) + "";
                var row = new string[] { DateTime.Now.ToString(), matricula.ToUpper(), nome.ToUpper(), marcador.ToUpper(), contadorDeProcessosPassados + "" };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
                paginaSEI.Fechar();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CarregarListaPeritos();
            CarregarListaMarcadores();

            comboBoxMatricula.DataSource = _listaPeritos.OrderBy(o => o.Nome).ToList();

            comboBoxMatricula.DisplayMember = "Nome";
            comboBoxMatricula.ValueMember = "Matricula";


            comboBoxMarcador.DataSource = _listaMarcadores.OrderBy(o => o).ToList();


        }

        private void CarregarListaPeritos()
        {
            _listaPeritos = new List<Perito>
            {
                new Perito() { Nome = "CLENIA", Matricula = "14306891" },
                new Perito() { Nome = "ANA PAULA", Matricula = "137303X" },
                new Perito() { Nome = "ANGELLE", Matricula = "16586255" },
                new Perito() { Nome = "MARCIA", Matricula = "2710528" },
                new Perito() { Nome = "GEORGE", Matricula = "2141612" },
                new Perito() { Nome = "ANTONIO JORGE", Matricula = "14312891" },
                new Perito() { Nome = "JULIANA", Matricula = "1860607" },
                new Perito() { Nome = "MARCELLO", Matricula = "00602710536" },
                new Perito() { Nome = "HUGO RAFAEL", Matricula = "214347X" },
                new Perito() { Nome = "ANA BORGES", Matricula = "137303X" },
                new Perito() { Nome = "LAIS FRANCA", Matricula = "1452304" },
                new Perito() { Nome = "ARCHIMEDES", Matricula = "2700468" },
                new Perito() { Nome = "MARIA FATIMA", Matricula = "02701316" },
                new Perito() { Nome = "THAYS", Matricula = "1947966" },
                new Perito() { Nome = "AMANDA", Matricula = "1748777" },
                new Perito() { Nome = "MICHELINE", Matricula = "1525441" },
                new Perito() { Nome = "CHRISTIANE", Matricula = "2143763" },
                new Perito() { Nome = "EVALDO", Matricula = "2701111" },
                new Perito() { Nome = "LIVIA", Matricula = "271051X" },
                new Perito() { Nome = "GUSTAVO", Matricula = "1735144" },
                new Perito() { Nome = "ALBERTO BRAGA", Matricula = "260082X" },
                new Perito() { Nome = "MARIA DE LOURDES(MALU)", Matricula = "2696282" },
                new Perito() { Nome = "PEDRO", Matricula = "1454722" },
                new Perito() { Nome = "FRANCISCO", Matricula = "2141752" },
                new Perito() { Nome = "CAMILA", Matricula = "14374390" },
                new Perito() { Nome = "ANTONIO CARLOS", Matricula = "1406299" },
                new Perito() { Nome = "JOYCE", Matricula = "1941615" },
                new Perito() { Nome = "MAURICIO", Matricula = "2600242" },
                new Perito() { Nome = "ANA KARINA", Matricula = "1542419" },
                new Perito() { Nome = "ROBERTO PAI ", Matricula = "2703556" },
                new Perito() { Nome = "JOSE GERALDO", Matricula = "1404466" },
                new Perito() { Nome = "GIANNA", Matricula = "1737805" },
                new Perito() { Nome = "ALEXANDRE OMENA", Matricula = "01948075" },
                new Perito() { Nome = "CLAUDIA MARIA", Matricula = "2699907" },
                new Perito() { Nome = "ROBERTO FILHO", Matricula = "1749218" },
                new Perito() { Nome = "SONY", Matricula = "02143488" },
                new Perito() { Nome = "RONEY NERY", Matricula = "02703661" },
                new Perito() { Nome = "EDVANIA", Matricula = "1748882" },
                new Perito() { Nome = "CAROLINE DA CUNHA", Matricula = "2626489" },
                new Perito() { Nome = "MILENA", Matricula = "2600129" },
                new Perito() { Nome = "YONA", Matricula = "1996568" },
                new Perito() { Nome = "CLAUDIA NARA", Matricula = "1305352" },
                new Perito() { Nome = "NILSON", Matricula = "1723324" },
                new Perito() { Nome = "RICARDO ANDRADE", Matricula = "2710501" },
                new Perito() { Nome = "TEREZA", Matricula = "16713435" },
                new Perito() { Nome = "ABELARDO", Matricula = "1968890" },
                new Perito() { Nome = "MARCUS", Matricula = "14309319" },
                new Perito() { Nome = "MARCELO RAMALHO", Matricula = "2710560" },
                new Perito() { Nome = "HELENA", Matricula = "2769727" },
                new Perito() { Nome = "ANGELA", Matricula = "17010942" },
                new Perito() { Nome = "MAGDA", Matricula = "2141949" },
                new Perito() { Nome = "GERALDA", Matricula = "2703521" },
                new Perito() { Nome = "ALEXANDRE GRIPP", Matricula = "2713004" },
                new Perito() { Nome = "CECILIA", Matricula = "1919539" },
                new Perito() { Nome = "RICARDO IBIAPINA", Matricula = "02143658" },
                new Perito() { Nome = "SIMONE", Matricula = "1654292" },
                new Perito() { Nome = "MARGARIDA", Matricula = "2733439" },
                new Perito() { Nome = "ANA LUCIA", Matricula = "2695243" },
                new Perito() { Nome = "CLAUDIO", Matricula = "1311433" },
                new Perito() { Nome = "CARINA", Matricula = "2143666" }
            };
        }

        private void comboBoxMatricula_TextUpdate(object sender, EventArgs e)
        {
            string filtro = comboBoxMatricula.Text.ToUpper();

            var novaListaPeritosFiltrada = _listaPeritos.FindAll(x => x.Nome.Contains(filtro)).OrderBy(o => o.Nome).ToList();

            comboBoxMatricula.DataSource = novaListaPeritosFiltrada.OrderBy(o => o.Nome).ToList();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                comboBoxMatricula.DataSource = _listaPeritos.OrderBy(o => o.Nome).ToList();
            }

            comboBoxMatricula.DroppedDown = true;
            Cursor.Current = Cursors.Default;

            // this will ensure that the drop down is as long as the list
            comboBoxMatricula.IntegralHeight = true;

            // remove automatically selected first item
            comboBoxMatricula.SelectedIndex = -1;

            comboBoxMatricula.Text = filtro;

            // set the position of the cursor
            comboBoxMatricula.SelectionStart = filtro.Length;
            comboBoxMatricula.SelectionLength = 0;
        }

        private void CarregarListaMarcadores()
        {
            _listaMarcadores = new List<string>()
            {
                "AZUL",
                "AMARELO",
                "VERDE",
                "VERMELHO",
            };
        }

        private void comboBoxMarcador_TextUpdate(object sender, EventArgs e)
        {
            string filtro = comboBoxMarcador.Text.ToUpper();

            var novaListaPeritosFiltrada = _listaMarcadores.FindAll(x => x.Contains(filtro)).OrderBy(o => o).ToList();

            comboBoxMarcador.DataSource = novaListaPeritosFiltrada.OrderBy(o => o).ToList();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                comboBoxMarcador.DataSource = _listaMarcadores.OrderBy(o => o).ToList();
            }

            comboBoxMarcador.DroppedDown = true;
            Cursor.Current = Cursors.Default;

            // this will ensure that the drop down is as long as the list
            comboBoxMarcador.IntegralHeight = true;

            // remove automatically selected first item
            comboBoxMarcador.SelectedIndex = -1;

            comboBoxMarcador.Text = filtro;

            // set the position of the cursor
            comboBoxMarcador.SelectionStart = filtro.Length;
            comboBoxMarcador.SelectionLength = 0;
        }
    }
}
