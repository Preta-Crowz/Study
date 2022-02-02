using System;

namespace CommandPattern {
    // Command - 명령 그 자체, Invoker에 의해 실행됨
    abstract class ICommand {
        // 임시로 만든 싱글톤 패턴, 실제로 쓸때는 각 Command가 Reciver를 지정받도록 하는것이 나을것
        public static Reciver _R = Reciver.GetInstance();
        // 각 명령은 Reciver로 정보를 넘겨주는 함수가 있음
        abstract public void Execute();
    }

    class StudyCommand : ICommand {
        public override void Execute() {
            _R.SetStatus("Study");
        }
    }

    class CatCommand : ICommand {
        public override void Execute() {
            _R.SetStatus("Meow");
        }
    }

    class PlayCommand : ICommand {
        public override void Execute() {
            _R.SetStatus("Play");
        }
    }

    class DisplayCommand : ICommand {
        public override void Execute() {
            _R.DisplayStatus();
        }
    }

    // Reciver - 실제로 명령을 받아 실행하는 객체, Command에 의해 동작함
    class Reciver {
        private static Reciver _instance = null;
        private string status;

        public static Reciver GetInstance() {
            if (_instance == null) _instance = new Reciver();
            return _instance;
        }

        private Reciver() {
            status = "None";
        }

        // 실제로 실행되는 부분
        public void SetStatus(string status) {
            this.status = status;
        }

        public void DisplayStatus() {
            Console.WriteLine($"Current status : {status}");
        }
    }

    // Invoker - Command를 전달받은 뒤, Command를 실행함
    class Invoker {
        // 분명 더 나은 방법이 있겠지만, 공부할때 최적화까지 너무 신경쓰면 머리 터질테니 생략함
        private ICommand StudyCmd;
        private ICommand CatCmd;
        private ICommand PlayCmd;
        private ICommand DisplayCmd;
        public Invoker(ICommand StudyCmd, ICommand CatCmd, ICommand PlayCmd, ICommand DisplayCmd) {
            this.StudyCmd = StudyCmd;
            this.CatCmd = CatCmd;
            this.PlayCmd = PlayCmd;
            this.DisplayCmd = DisplayCmd;
        }

        public void Study() => StudyCmd.Execute();
        public void Cat() => CatCmd.Execute();
        public void Play() => PlayCmd.Execute();
        public void Display() => DisplayCmd.Execute();
    }

    // Client - 주어진 상황에 맞게 Invoker에 어떤 Command를 전달하여 실행할지 결정함
    class Client {
        private Invoker _I;
        public Client() {
            _I = new Invoker(
                new StudyCommand(),
                new CatCommand(),
                new PlayCommand(),
                new DisplayCommand()
            );
        }
        // input을 보고 Invoker에서 명령 실행
        public void WorkToCommand(string Command) {
            switch (Command) {
                case "s": _I.Study(); return;
                case "c": _I.Cat(); return;
                case "p": _I.Play(); return;
                case "d": _I.Display(); return;
            }
        }

    }

    class Program {
        // 그래도 본체가 있어야 돌지..
        static void Main(string[] args) {
            Client _C = new Client();
            string cmd = null;
            while (true) {
                cmd = Console.ReadLine();
                _C.WorkToCommand(cmd);
            }
        }
    }
}
