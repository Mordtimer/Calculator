using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Calculator {
    class Cal {

        public string Equation { get; set; }

        #region methods
        public string Calculate() {
            DataTable result = new DataTable();
            string str_res;
            double double_res;
            
            GetFinalForm();
            try {
                str_res = Convert.ToString(result.Compute(Equation, String.Empty));
                double_res = Convert.ToDouble(str_res);
                str_res = double_res.ToString();
            }
            catch(SyntaxErrorException) {
                return "Are U OK?";
            }
            if(double_res == double.PositiveInfinity || double_res == double.NegativeInfinity)
                return "No way!";

            return str_res;
        }

        public void GetFinalForm() {

            //Close all unclosed brackets
            int open = 0, closed = 0;
            foreach(char el in Equation) {
                if(el == ')') closed++;
                else if(el == '(') open++;
            }
            for(int i = 0; i < (open - closed); i++)
                AddCharacter(')');

            //Remove last character in case of being bad operator
            if(non_brackets_operators.Contains(Equation[Equation.Length - 1]))
                RemoveLast();
        }

        public void AddCharacter(char element) {
            if(IsProper(ref element))
                Equation += element;
            else Equation = Equation;
        }

        public void ResetEquation() {
            Equation = "";
        }

        public void RemoveLast() {
            if (Equation.Length != 0)
                Equation = Equation.Remove(Equation.Length - 1);
        }

        public bool IsProper(ref char element) {

            switch(element) {
                
                case ')':
                    if(Equation[Equation.Length - 1] == '(')
                        return false;
                    int open = 0, closed = 0;
                    foreach(char el in Equation) {
                        if(el == ')') closed++;
                        else if(el == '(') open++;
                    }
                    if(open <= closed) return false;
                    break;
                
                case '(':
                    if(Equation == null || Equation.Length == 0) break;

                    if(!operators.Contains(Equation[Equation.Length-1]) 
                        || Equation[Equation.Length - 1] == '.' || Equation[Equation.Length - 1] == ')') 
                        return false;     
                    break;
                
                case ',':
                    element = '.';
                    break;

                case '.':
                    if(Equation == null || Equation.Length == 0)
                        AddCharacter('0'); 
                    else if(operators.Contains(Equation[Equation.Length - 1]) || Equation[Equation.Length - 1] == '.')
                        return false;
                    break;
                
                case '+':
                case '-':
                    if(Equation == null || Equation.Length == 0)
                        return false;
                    else if(Equation[Equation.Length - 1] == '.')
                        return false;
                    else if(non_brackets_operators.Contains(Equation[Equation.Length - 1]))
                        RemoveLast();
                    break;
                
                case '/':
                case '*':
                    if(Equation == null || Equation.Length == 0)
                        return false;
                    else if(Equation[Equation.Length - 1] == '.' || Equation[Equation.Length - 1] == '(')
                        return false;
                    else if(non_brackets_operators.Contains(Equation[Equation.Length - 1]))
                          RemoveLast();
                    break;

                default:
                    if(!(Equation == null || Equation.Length == 0))
                        if(Equation[Equation.Length - 1] == ')')
                            return false;
                    break;
            }
            return true;
        }

        #endregion

        #region operator lists

        private readonly List<char> operators = new List<char> {

            '-', '+', '*', '/', ')', '(',
        };

        private readonly List<char> non_brackets_operators = new List<char> {

            '-', '+', '*', '/'
        };

        #endregion
    }
}
