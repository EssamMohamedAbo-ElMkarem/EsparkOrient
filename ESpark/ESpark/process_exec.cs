using System;
using System.Diagnostics;

namespace ESpark
{
    class process_exec
    {

        public string prog_path;
        public string arguments;

        public void process()
        {
            ProcessStartInfo start_info = new ProcessStartInfo();
            start_info.FileName = prog_path;
            start_info.Arguments = arguments;
            start_info.CreateNoWindow = true;
            var process = new Process();
            process.StartInfo = start_info;
            process.Start();
            process.WaitForExit();
        }
        public process_exec(string program, string pro_arguments)
        {
            this.prog_path = program;
            this.arguments = pro_arguments;
        }
    }
}
