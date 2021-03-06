using Microsoft.Extensions.Options;
using SEI.Desktop.Models;
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
    }
}
