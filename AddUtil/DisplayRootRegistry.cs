using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddUtil
{
    public class DisplayRootRegistry
    {
        Dictionary<Type, Type> vmToWindowMapping = new Dictionary<Type, Type>();

        public void RegisterWindowType<VM, Win>() where Win : Window, new() where VM : class // Ограничение типа VM типом class - зачем оно? 
        {
            var viewModelType = typeof(VM);

            this.EnsureViewModelType(viewModelType);

            vmToWindowMapping[viewModelType] = typeof(Win);
        }

        public void UnregisterWindowType<VM>()
        {
            var viewModelType = typeof(VM);

            this.EnsureViewModelType(viewModelType);

            vmToWindowMapping.Remove(viewModelType);
        }

        public Window CreateWindowInstanceWithViewModel(object viewModel)
        {
            this.EnsureViewModelObject(viewModel);
            var viewModelType = viewModel.GetType();

            Type windowType = null;
            while (viewModelType != null && !vmToWindowMapping.TryGetValue(viewModelType, out windowType))
                viewModelType = viewModelType.BaseType;

            if (windowType is null)
                throw new ArgumentException($"No registered type {viewModelType.FullName} with window.");

            var window = (Window)Activator.CreateInstance(windowType);
            window.DataContext = viewModel;
            return window;
        }

        Dictionary<object, Window> openWindows = new Dictionary<object, Window>();
        public void ShowPresentation(object viewModel)
        {
            this.EnsureViewModelObject(viewModel);
            this.EnsureOpenedWindows(viewModel);

            Window window = this.CreateWindowInstanceWithViewModel(viewModel);
            window.Show();

            openWindows[viewModel] = window; // Теперь что ль put не нужен, он сам всё автоматом добавит что ль?
        }

        public void HidePresentation(object viewModel)
        {
            this.EnsureViewModelObject(viewModel);

            Window window;
            this.EnsureClosedWindows(viewModel, out window);

            window.Close();
            openWindows.Remove(window);
        }

        public async Task ShowModalPresentation(object viewModel)
        {
            var window = this.CreateWindowInstanceWithViewModel(viewModel);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }

        private void EnsureViewModelType(Type viewModelType)
        {
            if (viewModelType.IsInterface)
                throw new ArgumentException("Cannot register interfaces.");

            if (!vmToWindowMapping.ContainsKey(viewModelType))
                throw new InvalidOperationException($"Type {viewModelType.FullName} is not registered.");
        }

        private void EnsureViewModelObject(object viewModel)
        {
            if (viewModel is null)
                throw new ArgumentNullException("viewModel can't be null.");
        }

        private void EnsureOpenedWindows(object viewModel)
        {
            if (openWindows.ContainsKey(viewModel))
                throw new InvalidOperationException($"Already opened window with {viewModel.GetType().FullName} type.");
        }

        private void EnsureClosedWindows(object viewModel, out Window window)
        {
            if (!openWindows.TryGetValue(viewModel, out window))
                throw new InvalidOperationException("UI for this window isn't displayed.");
        }
    }
}
