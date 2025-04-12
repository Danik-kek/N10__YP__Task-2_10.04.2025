namespace N10__YP__Task_2_10._04._2025
{
    public partial class Form1 : Form
    {
        private float a = 0, b = 0;
        private int count = 0;

        // Словарь операций
        private Dictionary<int, Func<float, float, float>> operations = new()
        {
            { 1, (x, y) => x + y }, // Сложение
            { 2, (x, y) => x - y }, // Вычитание
            { 3, (x, y) => x * y }, // Умножение
            { 4, (x, y) => x / y }  // Деление
        };

        public Form1()
        {
            InitializeComponent();

            // Присвоение обработчиков для кнопок с числами
            foreach (var button in new[] { button13, button14, button15, button9,
                                         button10, button11, button5, button6,
                                         button7, button17 })
            {
                button.Click += (s, e) => textBox1.Text += ((Button)s).Text;
            }

            // Присвоение обработчиков для операций
            button4.Click += OperationButton_Click; // "+"
            button8.Click += OperationButton_Click; // "-"
            button12.Click += OperationButton_Click; // "*"
            button16.Click += OperationButton_Click; // "/"

            // Прочие обработчики
            button1.Click += (s, e) => ToggleSign(); // "+/-"
            button2.Click += (s, e) => Backspace(); // "<--"
            button3.Click += (s, e) => Clear();     // "C"
            button18.Click += (s, e) => AddDecimalPoint(); // "."
            button19.Click += (s, e) => CalculateResult(); // "="
        }

        // Обработчик для кнопок операций
        private void OperationButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                a = float.Parse(textBox1.Text);
                textBox1.Clear();
                count = GetOperationId(button.Text); // Получаем ID операции
                label1.Text = a.ToString() + button.Text;
            }
        }

        // Получение ID операции по символу
        private int GetOperationId(string operation)
        {
            return operation switch
            {
                "+" => 1,
                "-" => 2,
                "*" => 3,
                "/" => 4,
                _ => 0
            };
        }

        // Выполнение операции
        private void PerformOperation(int operationId)
        {
            if (operations.TryGetValue(operationId, out var operation))
            {
                b = operation(a, float.Parse(textBox1.Text));
                textBox1.Text = b.ToString();
            }
        }

        // Расчет результата
        private void CalculateResult()
        {
            PerformOperation(count);
            label1.Text = "";
        }

        // Переключение знака числа
        private void ToggleSign()
        {
            textBox1.Text = textBox1.Text.StartsWith("-")
                ? textBox1.Text[1..]
                : $"-{textBox1.Text}";
        }

        // Очистка поля ввода
        private void Clear()
        {
            textBox1.Text = "";
            label1.Text = "";
            a = 0;
            b = 0;
            count = 0;
        }

        // Удаление последнего символа
        private void Backspace()
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = textBox1.Text[..^1];
            }
        }

        // Добавление десятичной точки
        private void AddDecimalPoint()
        {
            if (!textBox1.Text.Contains(",") && !textBox1.Text.Contains("."))
            {
                textBox1.Text += ",";
            }
        }
    }
}