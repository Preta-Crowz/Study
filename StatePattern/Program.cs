using System;

namespace StatePattern {
    // State의 슈퍼클래스
    abstract class IState {
        public abstract int calc(Context ctx, int a, int b);
    }

    // State - 각각의 상태와 그 상태에 맞는 처리 함수
    class AddState : IState {
        public override int calc(Context ctx, int a, int b) {
            ctx.SetState(new SubState());
            return a + b;
        }
    }

    class SubState : IState {
        public override int calc(Context ctx, int a, int b) {
            ctx.SetState(new MulState());
            return a - b;
        }
    }

    class MulState : IState {
        public override int calc(Context ctx, int a, int b) {
            ctx.SetState(new DivState());
            return a * b;
        }
    }

    class DivState : IState {
        public override int calc(Context ctx, int a, int b) {
            ctx.SetState(new AddState());
            return a / b;
        }
    }

    // Context - 상태를 저장해두고, 실행함
    class Context {
        private IState State;
        public Context() {
            this.SetState(new AddState());
        }

        // 상태를 변경하는 함수
        public void SetState(IState state) {
            this.State = state;
        }

        // 저장되어있는 상태의 처리 함수 실행
        public int calc(int a, int b) => State.calc(this, a, b);
    }

    // 본체
    class Program {
        static void Main(string[] args) {
            int a = 0;
            int b = 0;
            Context ctx = new Context();
            int r = 0;
            while (true) {
                a = int.Parse(Console.ReadLine());
                b = int.Parse(Console.ReadLine());
                r = ctx.calc(a, b);
                Console.WriteLine(r);
            }
        }
    }
}
