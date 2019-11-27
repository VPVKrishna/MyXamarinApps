using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculatorApp
{
    public partial class CalculationPage : ContentPage
    {
        public CalculationPage()
        {
            InitializeComponent();
        }

        void ButtonClickEvent(object sender, EventArgs args)
        {

            Button button = sender as Button;

            string input = button.Text;
            switch (input)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if ((entryTotal.Text!="0" && entryTotal.Text!="") && prevOpr == "")
                        return;

                    entryCurrent.Text = entryCurrent.Text + "" + input;
                    break;
                case "C":
                    prevOpr = "";
                    entryOpr.Text = "";
                    entryCurrent.Text = ""; entryTotal.Text = "0";
                    break;
                case "+/-":
                    if (entryCurrent.Text.StartsWith("-"))
                        entryCurrent.Text = entryCurrent.Text.Substring(1);
                    else
                        entryCurrent.Text = "-" + entryCurrent.Text;
                    break;
                case ".":
                    if ((entryTotal.Text != "0" && entryTotal.Text != "") && prevOpr == "")
                        return;
                    if (!entryCurrent.Text.Contains("."))
                        entryCurrent.Text = entryCurrent.Text + ".";
                    break;
                default:
                    if (entryCurrent.Text == "." || entryCurrent.Text == "-")
                        return;
                    DoOperation(input);
                    break;
            }

            if (entryCurrent.Text.Length > 0 && (entryCurrent.Text.StartsWith("0") || entryCurrent.Text.StartsWith("0.")))
            {
                entryCurrent.Text = entryCurrent.Text.Substring(1);
            }
        }

        string prevOpr = "";
        public void DoOperation(string opr)
        {
            double totalValue = 0;
            if (entryTotal.Text.Length > 0)
            {
                totalValue = Convert.ToDouble(entryTotal.Text);
            }

            double value = 0;
            if (entryCurrent.Text.Length > 0)
            {
                value = Convert.ToDouble(entryCurrent.Text);
            }

            if (totalValue != 0)
            {
                double calcValue = Caluculate(totalValue, value, prevOpr);

                Console.WriteLine("\n" + totalValue + " " + prevOpr + " " + value + "  =  " + calcValue + "   " + opr + " ");

                value = calcValue;
            }

            prevOpr = opr;

            if (prevOpr == "=")
                prevOpr = "";

            entryOpr.Text = prevOpr;
            entryTotal.Text = Convert.ToString(value);
            entryCurrent.Text = "0";

            if (entryCurrent.Text.Length > 2 && entryCurrent.Text.EndsWith(".0"))
                entryCurrent.Text = entryCurrent.Text.Substring(1, entryCurrent.Text.Length - 2);
        }

        public double Caluculate(double operand1, double operand2, string opr)
        {
            switch (opr)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                case "X":
                    return operand1 * operand2;
                case "%":
                    return operand1 % operand2;
                case "DIV":
                    return operand1 / operand2;
                case "=":
                case "":
                    if (operand2 == 0) return operand1;
                    return operand2;
            }

            Console.WriteLine("PLEASE Handle :" + opr);

            throw new Exception("Handle this operation");
        }
    }
}
