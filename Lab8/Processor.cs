using System;

namespace Lab8
{
    public enum TOprtn
    {
        None,
        Add,
        Sub,
        Mul,
        Dvd
    }

    public enum TFunc
    {
        Rev,
        Sqr
    }

    public class TProc<T> where T : new()
    {
        private T lopRes;
        private T rop;
        private TOprtn operation;

        public T Lop_Res
        {
            get { return CopyValue(lopRes); }
            set { lopRes = CopyValue(value); }
        }

        public T Rop
        {
            get { return CopyValue(rop); }
            set { rop = CopyValue(value); }
        }

        public TOprtn Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        public TProc()
        {
            lopRes = new T();
            rop = new T();
            operation = TOprtn.None;
        }

        public void ReSet()
        {
            lopRes = new T();
            rop = new T();
            operation = TOprtn.None;
        }

        public void OprtnClear()
        {
            operation = TOprtn.None;
        }

        public void OprtnRun()
        {
            if (operation == TOprtn.None)
                return;

            dynamic a = lopRes;
            dynamic b = rop;

            switch (operation)
            {
                case TOprtn.Add:
                    lopRes = a + b;
                    break;
                case TOprtn.Sub:
                    lopRes = a - b;
                    break;
                case TOprtn.Mul:
                    lopRes = a * b;
                    break;
                case TOprtn.Dvd:
                    if (b == 0)
                        throw new DivideByZeroException("Деление на ноль");
                    lopRes = a / b;
                    break;
            }
        }

        public void FuncRun(TFunc func)
        {
            dynamic a = rop;

            switch (func)
            {
                case TFunc.Rev:
                    if (a == 0)
                        throw new InvalidOperationException("Обратное значение для нуля не существует");
                    rop = 1 / a;
                    break;
                case TFunc.Sqr:
                    rop = a * a;
                    break;
            }
        }

        public void OprtnSet(TOprtn oprtn)
        {
            operation = oprtn;
        }

        public void Lop_Res_Set(T value)
        {
            lopRes = CopyValue(value);
        }

        public void Rop_Set(T value)
        {
            rop = CopyValue(value);
        }

        private T CopyValue(T value)
        {
            if (typeof(T).IsValueType)
                return value;
            else
                return (T)(value as ICloneable)?.Clone() ?? value;
        }
    }
}
