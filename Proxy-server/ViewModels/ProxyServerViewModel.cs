using Proxy_server.Proxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proxy_server.ViewModels
{
    class ProxyServerViewModel : ViewModelBase
    {
        private Server _server;

        private readonly CommandDelegate _startStopProxyCommand;
        private readonly CommandDelegate _clearLog;
        public ICommand StartStopProxyCommand => _startStopProxyCommand;
        public ICommand ClearLogCommand => _clearLog;

        public ProxyServerViewModel()
        {
            _server = new Server();
            _startStopProxyCommand = new CommandDelegate(OnStartStopProxy, CanStartStopProxy);
            _clearLog = new CommandDelegate(OnClearLog, CanClearLog);
        }

        public bool IsRunning
        {
            get => _server.IsRunning;
            set => SetProperty(_server.IsRunning, value, () => _server.IsRunning = value);
        }

        public int ProxyPort
        {
            get => _server.Port;
            set => SetProperty(_server.Port, value, () => _server.Port = value);
        }

        public int ProxyCacheTimeOutInSeconds
        {
            get => _server.CacheTimeOutInSeconds;
            set => SetProperty(_server.CacheTimeOutInSeconds, value, () => _server.CacheTimeOutInSeconds = value);
        }

        public int ProxyBufferSize
        {
            get => _server.BufferSize;
            set => SetProperty(_server.BufferSize, value, () => _server.BufferSize = value);
        }

        public bool ProxyHideUserAgentEnabled
        {
            get => _server.HideUserAgentEnabled;
            set => SetProperty(_server.HideUserAgentEnabled, value, () => _server.HideUserAgentEnabled = value);
        }

        public bool ProxyFilterContentEnabled
        {
            get => _server.FilterContentEnabled;
            set => SetProperty(_server.FilterContentEnabled, value, () => _server.FilterContentEnabled = value);
        }

        public bool ProxyLogRequest
        {
            get => _server.LogRequest;
            set => SetProperty(_server.LogRequest, value, () => _server.LogRequest = value);
        }

        public bool ProxyLogResponse
        {
            get => _server.LogResponse;
            set => SetProperty(_server.LogResponse, value, () => _server.LogResponse = value);
        }

        public ObservableCollection<ListBoxItem> Log
        {
            get => _server.Log;
        }

        private async void OnStartStopProxy(object commandParameter)
        {
            if (!_server.IsRunning)
            {
                
                await Task.Run(() => _server.Start());
                NotifyPropertyChanged("IsRunning");
            }
            else
            {
                await Task.Run(() => _server.Stop());
                NotifyPropertyChanged("IsRunning");
            }
        }

        private bool CanStartStopProxy(object commandParameter) => true;
        public void OnClearLog(object commandParameter) => _server.ClearLog();
        private bool CanClearLog(object commandParameter) => true;
    }
}
