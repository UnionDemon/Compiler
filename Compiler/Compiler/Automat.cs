using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Automat
    {
        private int state;
        private int firstNumber;
        private int secondNumber;
        private int result;
        private bool isNegative;
        private bool skip;
        private char operation;

        private bool isCorrect()
        {
            if (skip)
            {
                return false;
            }
            
            if (operation == '+')
            {
                return (firstNumber + secondNumber == result);
            }

            if (operation == '-')
            {
                return (firstNumber - secondNumber == result);
            }
            
            if (operation == '*')
            {
                return (firstNumber * secondNumber == result);
            }

            if (operation == '/')
            {
                if (secondNumber == 0)
                {
                    throw new Exception("Обнаружено деление на 0.");
                }

                return (firstNumber / secondNumber == result);
            }

            if (operation == '^')
            {
                if (secondNumber < 0)
                {
                    throw new Exception("Обнаружено возведение в отрицательную степень.");
                }
                return (((int)Math.Pow(firstNumber,secondNumber)) == result);
            }

            throw new Exception("Неизвестный знак операции: '" + operation + "'.");
        }

        private String charView(char c)
        {
            if (c == '\0')
            {
                return "EOF";
            }
            else
            {
                return "" + c;
            }
        }

        public bool Check(CharChain chain)
        {
            state = 0;
            firstNumber = 0;
            secondNumber = 0;
            result = 0;
            isNegative = false;
            skip = false;
            operation = '\0';

            while (state != 13)
            {
                switch (state)
                {
                    case 0:
                        state0(chain);
                        break;
                    case 1:
                        state1(chain);
                        break;
                    case 2:
                        state2(chain);
                        break;
                    case 3:
                        state3(chain);
                        break;
                    case 4:
                        state4(chain);
                        break;
                    case 5:
                        state5(chain);
                        break;
                    case 6:
                        state6(chain);
                        break;
                    case 7:
                        state7(chain);
                        break;
                    case 8:
                        state8(chain);
                        break;
                    case 9:
                        state9(chain);
                        break;
                    case 10:
                        state10(chain);
                        break;
                    case 11:
                        state11(chain);
                        break;
                    case 12:
                        state12(chain);
                        break;
                }
            }
            return isCorrect();

        }

        private bool isOperation(char c)
        {
            switch (c)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                case '^':
                    return true;
                default:
                    return false;
            }
        }

        private void state0(CharChain chain)
        {
            char next = chain.GetNext();

            if(next == '0')
            {
                state = 3;
            }
            else if (next >= '1' && next <= '9')
            {
                state = 1;

                int digit = ((int)next) - ((int)'0');
                firstNumber = firstNumber * 10 + digit;
            }
            else if (next == '-')
            {
                state = 2;
                
                isNegative = true;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась цифра.");
            }
        }

        private void state1(CharChain chain)
        {
            char next = chain.GetNext();

            if (next >= '0' && next <= '9')
            {
                state = 1;

                int digit = ((int)next) - ((int)'0');
                firstNumber = firstNumber * 10 + digit;
            }
            else if (isOperation(next))
            {
                state = 4;

                operation = next;
                if (isNegative)
                {
                    firstNumber *= -1;
                }
                isNegative = false;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась операция.");
            }
        }

        private void state2(CharChain chain)
        {
            char next = chain.GetNext();

            if (next >= '1' && next <= '9')
            {
                state = 1;

                int digit = ((int)next) - ((int)'0');
                firstNumber = firstNumber * 10 + digit;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась цифра.");
            }
        }

        private void state3(CharChain chain)
        {
            char next = chain.GetNext();

            if(isOperation(next))
            {
                state = 4;

                operation = next;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидался знак операции.");
            }
        }

        private void state4(CharChain chain)
        {
            char next = chain.GetNext();

            if (next == '0')
            {
                state = 7;
            }
            else if (next >= '1' && next <= '9')
            {
                state = 5;

                int digit = ((int)next) - ((int)'0');
                secondNumber = secondNumber * 10 + digit;
            }
            else if (next == '-')
            {
                state = 6;

                isNegative = true;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась цифра.");
            }
        }

        private void state5(CharChain chain)
        {
            char next = chain.GetNext();

            if (next >= '0' && next <= '9')
            {
                state = 5;

                int digit = ((int)next) - ((int)'0');
                secondNumber = secondNumber * 10 + digit;
            }
            else if (next == '=')
            {
                state = 8;

                if (isNegative)
                {
                    secondNumber *= -1;
                }
                isNegative = false;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалось '='.");
            }
        }

        private void state6(CharChain chain)
        {
            char next = chain.GetNext();

            if (next >= '1' && next <= '9')
            {
                state = 5;

                int digit = ((int)next) - ((int)'0');
                secondNumber = secondNumber * 10 + digit;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась цифра.");
            }
        }

        private void state7(CharChain chain)
        {
            char next = chain.GetNext();

            if (next == '=')
            {
                state = 8;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидался '='.");
            }
        }

        private void state8(CharChain chain)
        {
            char next = chain.GetNext();

            if (next == '0')
            {
                state = 11;
            }
            else if (next >= '1' && next <= '9')
            {
                state = 9;

                int digit = ((int)next) - ((int)'0');
                result = result * 10 + digit;
            }
            else if (next == '-')
            {
                state = 10;

                isNegative = true;
            }
            else if (next == '_')
            {
                state = 12;

                skip = true;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась цифра.");
            }
        }

        private void state9(CharChain chain)
        {
            char next = chain.GetNext();

            if (next >= '0' && next <= '9')
            {
                state = 9;

                int digit = ((int)next) - ((int)'0');
                result = result * 10 + digit;
            }
            else if (next == '\n' || next == '\0')
            {
                state = 13;

                if (isNegative)
                {
                    result *= -1;
                }
                isNegative = false;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидался перенос строки или конец файла.");
            }
        }
        private void state10(CharChain chain)
        {
            char next = chain.GetNext();

            if (next >= '1' && next <= '9')
            {
                state = 9;

                int digit = ((int)next) - ((int)'0');
                result = result * 10 + digit;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидалась цифра.");
            }
        }
        private void state11(CharChain chain)
        {
            char next = chain.GetNext();

            if (next == '\n' || next == '\0')
            {
                state = 13;
            }
            else
            {
                throw new Exception("Обнаружен символ: '" + charView(next) + "', ожидался перенос строки или конец файла.");
            }
        }

        private void state12(CharChain chain)
        {
            char next = chain.GetNext();

            if (next == '\n' || next == '\0')
            {
                state = 13;
            }
            else
            {
                throw new Exception("Обнаружен символ : '" + charView(next) + "', ожидался перенос строки или конец файла.");
            }
        }
    }
}