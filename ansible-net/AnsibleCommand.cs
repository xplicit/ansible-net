using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ansible
{
    public abstract class AnsibleCommand
    {
        public abstract string CommandName { get; }

        public virtual string WorkingDirectory { get; set; }

        protected Dictionary<string, string> Options = new Dictionary<string, string>();

        public virtual void Execute(Action<string> onOutput = null, Action<string> onError = null)
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = this.WorkingDirectory,
                FileName = CommandName,
                Arguments = CreateCommandLine(),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            var commandProcess = new Process { StartInfo = startInfo };
            commandProcess.OutputDataReceived += (sender, args) => onOutput?.Invoke(args.Data);
            commandProcess.ErrorDataReceived += (sender, args) => onError?.Invoke(args.Data);
            commandProcess.Start();

            commandProcess.BeginOutputReadLine();
            commandProcess.BeginErrorReadLine();

            commandProcess.WaitForExit();
        }

        protected virtual string CreateCommandLine()
        {
            var args = string.Empty;

            foreach(var key in Options.Keys)
            {
                args += args.Length >0 ? (" " + key) : key;

                //TODO: need escaping
                if (!string.IsNullOrEmpty(Options[key]))
                    args += "=" + Options[key];
            }

            return args;
        }

        public virtual AnsibleCommand Version() => AddParameter("--version");

        public virtual AnsibleCommand AskBecomePassword() => AddParameter("--ask-become-pass");

        public virtual AnsibleCommand AskConnectionPassword() => AddParameter("--ask-pass");

        public virtual AnsibleCommand AskVaultPassword() => AddParameter("--ask-vault-pass");

        public virtual AnsibleCommand RunAsAsync(int timeout) => AddParameter("--background", timeout.ToString());

        public virtual AnsibleCommand Become(BecomeMethod method = BecomeMethod.sudo, string user = "root") =>
            AddParameter("--become")
            .AddParameter("--become-method", method.ToString().ToLowerInvariant())
            .AddParameter("--become-user", user);

        public virtual AnsibleCommand Check() => AddParameter("--check");

        public virtual AnsibleCommand Connection(string connectionType = "smart") => AddParameter("--connection", connectionType);

        public virtual AnsibleCommand Diff() => AddParameter("--diff");

        public virtual AnsibleCommand ExtraVariables(string vars) => AddParameter("--extra-vars", vars);

        public virtual AnsibleCommand Forks(int n) => AddParameter("--forks", n.ToString());

        public virtual AnsibleCommand Help() => AddParameter("--help");

        public virtual AnsibleCommand InventoryHosts(string filename) => AddParameter("--inventory", filename);

        public virtual AnsibleCommand LimitHosts(string pattern) => AddParameter("--limit", pattern);

        public virtual AnsibleCommand ListHosts() => AddParameter("--list-hosts");

        public virtual AnsibleCommand ModuleName(string name) => AddParameter("--module-name", name);

        public virtual AnsibleCommand ModulePath(string path) => AddParameter("--module-path", path);

        public virtual AnsibleCommand NewVaultPasswordFile(string filename) => AddParameter("--new-vault-password-file", filename);

        public virtual AnsibleCommand OneLine() => AddParameter("--one-line");

        public virtual AnsibleCommand OutputFile(string filename) => AddParameter("--output", filename);
        
        public virtual AnsibleCommand PollInterval(int interval) => AddParameter("--poll-interval", interval.ToString());

        public virtual AnsibleCommand PrivateKey(string filename) => AddParameter("--private-key", filename);

        public virtual AnsibleCommand ScpExtraArguments(string args) => AddParameter("--scp-extra-args", args);

        public virtual AnsibleCommand SftpExtraArguments(string args) => AddParameter("--sftp-extra-args", args);

        public virtual AnsibleCommand SshCommonArguments(string args) => AddParameter("--ssh-common-args", args);

        public virtual AnsibleCommand SshExtraArguments(string args) => AddParameter("--ssh-extra-args", args);

        public virtual AnsibleCommand SyntaxCheck() => AddParameter("--syntax-check");

        public virtual AnsibleCommand ConnectionTimeout(int seconds) => AddParameter("--timeout", seconds.ToString());

        public virtual AnsibleCommand LogOutput(string path) => AddParameter("--tree", path);

        public virtual AnsibleCommand User(string remoteUser) => AddParameter("--user", remoteUser);

        public virtual AnsibleCommand VaultPasswordFile(string filename) => AddParameter("--vault-password-file", filename);

        public virtual AnsibleCommand Verbose(VerboseLevel level = VerboseLevel.Low)
        {
            switch (level)
            {
                case VerboseLevel.Low:
                   return AddParameter("-v"); 
                case VerboseLevel.Medium:
                   return AddParameter("-vv");
                case VerboseLevel.High:
                   return AddParameter("-vvv");
                case VerboseLevel.Highest:
                   return AddParameter("-vvvv");
            }

            return this; 
        }
        
        public virtual AnsibleCommand AddParameter(string param, string value = null)
        {
            if (Options.ContainsKey(param))
                Options[param] = value;
            else
                Options.Add(param, value);
            return this;
        }
    }
}
