using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab10
{
    public class TMember
    {
        public int Coeff { get; set; }
        public int Degree { get; set; }

        public TMember(int coeff = 0, int degree = 0)
        {
            Coeff = coeff;
            Degree = degree;
        }

        public bool Equals(TMember other)
        {
            return Coeff == other.Coeff && Degree == other.Degree;
        }

        public TMember Differentiate()
        {
            if (Degree == 0)
                return new TMember(0, 0);
            return new TMember(Coeff * Degree, Degree - 1);
        }

        public double Evaluate(double x)
        {
            return Coeff * Math.Pow(x, Degree);
        }

        public override string ToString()
        {
            return $"{Coeff}*X^{Degree}";
        }
    }

    public class TPoly
    {
        private List<TMember> members = new List<TMember>();

        public TPoly(int coeff = 0, int degree = 0)
        {
            if (coeff != 0)
                members.Add(new TMember(coeff, degree));
        }

        public int Degree()
        {
            if (!members.Any()) return 0;
            return members.Where(m => m.Coeff != 0).Max(m => m.Degree);
        }

        public int Coeff(int degree)
        {
            var member = members.FirstOrDefault(m => m.Degree == degree);
            return member?.Coeff ?? 0;
        }

        public void Clear()
        {
            members.Clear();
        }

        public TPoly Add(TPoly other)
        {
            var result = new TPoly();
            result.members.AddRange(this.members);
            result.members.AddRange(other.members);
            result.Normalize();
            return result;
        }

        public TPoly Subtract(TPoly other)
        {
            return this.Add(other.Negate());
        }

        public TPoly Multiply(TPoly other)
        {
            var result = new TPoly();
            foreach (var m1 in this.members)
                foreach (var m2 in other.members)
                    result.members.Add(new TMember(m1.Coeff * m2.Coeff, m1.Degree + m2.Degree));
            result.Normalize();
            return result;
        }

        public TPoly Negate()
        {
            var result = new TPoly();
            foreach (var m in this.members)
                result.members.Add(new TMember(-m.Coeff, m.Degree));
            result.Normalize();
            return result;
        }

        public bool Equals(TPoly other)
        {
            this.Normalize();
            other.Normalize();
            if (this.members.Count != other.members.Count) return false;
            for (int i = 0; i < this.members.Count; i++)
                if (!this.members[i].Equals(other.members[i]))
                    return false;
            return true;
        }

        public TPoly Differentiate()
        {
            var result = new TPoly();
            foreach (var m in this.members)
            {
                var diff = m.Differentiate();
                if (diff.Coeff != 0)
                    result.members.Add(diff);
            }
            result.Normalize();
            return result;
        }

        public double Evaluate(double x)
        {
            return members.Sum(m => m.Evaluate(x));
        }

        public TMember GetMember(int index)
        {
            if (index < 0 || index >= members.Count)
                throw new IndexOutOfRangeException();
            return members[index];
        }

        public int MemberCount()
        {
            return members.Count;
        }

        private void Normalize()
        {
            RemoveZeros();
            SortByDegree();
            CombineLikeTerms();
        }

        private void RemoveZeros()
        {
            members.RemoveAll(m => m.Coeff == 0);
        }

        private void SortByDegree()
        {
            members = members.OrderBy(m => m.Degree).ToList();
        }

        private void CombineLikeTerms()
        {
            if (!members.Any()) return;
            var result = new List<TMember>();
            TMember current = members[0];

            for (int i = 1; i < members.Count; i++)
            {
                if (members[i].Degree == current.Degree)
                {
                    current.Coeff += members[i].Coeff;
                }
                else
                {
                    if (current.Coeff != 0)
                        result.Add(current);
                    current = members[i];
                }
            }
            if (current.Coeff != 0)
                result.Add(current);
            members = result;
        }

        public override string ToString()
        {
            if (!members.Any()) return "0*X^0";
            return string.Join(" + ", members.Select(m => m.ToString()));
        }
    }
}