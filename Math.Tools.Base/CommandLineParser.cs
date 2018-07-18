using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.Tools.Base
{
    public class CommandLineParser
    {
        private readonly IDictionary<string, string[]> _args;
        private readonly IList<string> _error;
        private readonly IList<string> _optionText;
        private readonly IList<string> _helpText;
        private readonly string _cmd;
        private Action<string> _helpAction;
        private Action<string, string> _errorAction;

        public CommandLineParser(string cmd, IEnumerable<string> args)
        {
            _helpAction = null;
            _errorAction = null;
            _cmd = cmd;
            _args = new Dictionary<string, string[]>();
            _error = new List<string>();
            _optionText = new List<string>();
            _helpText = new List<string>();
            var currentName = "";
            var values = new List<string>();
            foreach (var arg in args)
            {
                if (arg.StartsWith("-", StringComparison.Ordinal))
                {
                    if (currentName != "")
                        _args[currentName] = values.ToArray();
                    values.Clear();
                    currentName = arg.Substring(1);
                }
                else if (currentName == "")
                    _args[arg] = new string[0];
                else
                    values.Add(arg);
            }

            if (currentName != "")
                _args[currentName] = values.ToArray();
        }

        public bool IsHelp()
        {
            return _args.ContainsKey("help") || _args.ContainsKey("?") || (!_args.Any() && _error.Any());
        }

        public bool IsError()
        {
            return _error.Any();
        }

        public string GetError()
        {
            return string.Join(" ", _error);
        }

        private string GetHelp()
        {
            return _cmd + " " + string.Join(" ", _optionText) + "\n\n" + string.Join("\n", _helpText);
        }

        public CommandLineParser Setup<T>(string option, string description, out T s) where T : struct, IConvertible
        {
            s = new T();
            _optionText.Add("[-" + option + " <string>]");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                try
                {
                    s = EnumUtils.ParseEnum(_args["server"][0], new T());
                }
                catch
                {
                    _error.Add("missing : " + _optionText.Last());
                }
            }

            return this;
        }

        public CommandLineParser Setup<T>(string option, string description, out T s, T init)
            where T : struct, IConvertible
        {
            s = init;
            _optionText.Add("[-" + option + " <string>]");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                try
                {
                    s = EnumUtils.ParseEnum(_args["server"][0], init);
                }
                catch
                {
                    // ignored
                }
            }

            return this;
        }


        public CommandLineParser Setup(string option, string description, out string s)
        {
            s = null;
            _optionText.Add("-" + option + " <string>");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                s = _args["server"][0];
            }
            else
            {
                _error.Add("missing : " + _optionText.Last());
            }

            return this;
        }

        public CommandLineParser Setup(string option, string description, out string s, string init)
        {
            s = init;
            _optionText.Add("[-" + option + " <string>]");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                s = _args["server"][0];
            }

            return this;
        }

        public CommandLineParser Setup(string option, string description, out double d)
        {
            d = double.NaN;
            _optionText.Add("-" + option + " <double>");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                if (double.TryParse(_args["server"][0], out var y))
                {
                    d = y;
                }
                else
                {
                    _error.Add("parsing error : " + _optionText.Last());
                }
            }
            else
            {
                _error.Add("missing : " + _optionText.Last());
            }

            return this;
        }

        public CommandLineParser Setup(string option, string description, out double d, double init)
        {
            d = init;
            _optionText.Add("[-" + option + " <double>]");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                if (double.TryParse(_args["server"][0], out var y))
                {
                    d = y;
                }
                else
                {
                    _error.Add("parsing error : " + _optionText.Last());
                }
            }

            return this;
        }

        public CommandLineParser Setup(string option, string description, out int i)
        {
            i = 0;
            _optionText.Add("-" + option + " <int>");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                if (int.TryParse(_args["server"][0], out var y))
                {
                    i = y;
                }
                else
                {
                    _error.Add("parsing error : " + _optionText.Last());
                }
            }
            else
            {
                _error.Add(_optionText.Last());
            }

            return this;
        }

        public CommandLineParser Setup(string option, string description, out int i, int init)
        {
            i = init;
            _optionText.Add("[-" + option + " <int>]");
            _helpText.Add(_optionText.Last() + " : " + description);
            if (_args.ContainsKey(option))
            {
                if (int.TryParse(_args["server"][0], out var y))
                {
                    i = y;
                }
                else
                {
                    _error.Add("parsing error : " + _optionText.Last());
                }
            }

            return this;
        }

        public CommandLineParser SetupHelp(Action<string> action)
        {
            _helpAction = action;
            return this;
        }

        public CommandLineParser SetupError(Action<string, string> action)
        {
            _errorAction = action;
            return this;
        }

        public bool Parse()
        {
            if (IsHelp() && _helpAction != null)
            {
                _helpAction(GetHelp());
            }
            else if (IsError() && _errorAction != null)
            {
                _errorAction(GetHelp(), GetError());
            }

            return IsError();
        }
    }
}