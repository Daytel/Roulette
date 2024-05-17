using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roulette
{
    public partial class Game : Form
    {
        Dictionary<string, int> you_items, dealer_items, cur_items; // Словарь предметов
        int you_hp, dealer_hp; public int max_hp; // Число HP
        int liveround, blank; // Число боевых и холостых патронов
        ListBox curlistbox; // Отображение текущего массива предметов
        public Game()
        {
            InitializeComponent();
            // Создаём словари предметов
            you_items = new Dictionary<string, int>() {{ "Adrenalin", 0 }, { "Saw", 0 }, { "Telephone", 0 },
                { "Magnifer", 0}, { "Inverter", 0}, { "Handcuffs", 0}, { "Beer", 0}, { "Cigarettes", 0}, { "Medicine", 0 } };
            dealer_items = new Dictionary<string, int>() {{ "Adrenalin", 0 }, { "Saw", 0 }, { "Telephone", 0 },
                { "Magnifer", 0}, { "Inverter", 0}, { "Handcuffs", 0}, { "Beer", 0}, { "Cigarettes", 0}, { "Medicine", 0 } };
            // Определеем предмет заполнения
            cur_items = you_items;
            curlistbox = Your_Item;
            // Задаём число патрон
            liveround = 1; blank = 1;
            LifeRoundCount.Text = liveround.ToString();
            BlankCount.Text = blank.ToString();
            // Задаём HP по умолчанию
            MaxHP.Controls.Add(HP2); MaxHP.Controls.Add(HP3); MaxHP.Controls.Add(HP4);
            max_hp = 2;
            you_hp = max_hp; dealer_hp = max_hp;
        }
        private void Item_Click(object sender, EventArgs e)
        {
            Button cur_button = (Button)sender; // Инициализируем кнопку предмета
            if (curlistbox.Items.Count != 8)
            {
                cur_items[cur_button.Name]++;
                curlistbox.Items.Add(cur_button.Name);
            }
        }
        private void Change_Array_Item(object sender, EventArgs e)
        {
            ListBox cur_listbox = (ListBox)sender; // Инициализируем массив предметов
            if (cur_listbox.Name == Your_Item.Name)
            {
                cur_items = you_items;
                curlistbox = Your_Item;
            }
            else
            {
                cur_items = dealer_items;
                curlistbox = Dealer_Item;
            }
        }
        private void Array_Clear(object sender, EventArgs e)
        {
            if (cur_items == you_items)
            {
                you_items = new Dictionary<string, int>() {{ "Adrenalin", 0 }, { "Saw", 0 }, { "Telephone", 0 },
                { "Magnifer", 0}, { "Inverter", 0}, { "Handcuffs", 0}, { "Beer", 0}, { "Cigarettes", 0}, { "Medicine", 0 } };
                Your_Item.Items.Clear();
            }
            else
            {
                dealer_items = new Dictionary<string, int>() {{ "Adrenalin", 0 }, { "Saw", 0 }, { "Telephone", 0 },
                { "Magnifer", 0}, { "Inverter", 0}, { "Handcuffs", 0}, { "Beer", 0}, { "Cigarettes", 0}, { "Medicine", 0 } };
                Dealer_Item.Items.Clear();
            }
        }
        private void Add_Patron(object sender, EventArgs e)
        {
            Button cur_button = (Button)sender; // Инициализируем кнопку патрона
            if (cur_button.Name == LifeRoundButton.Name)
            {
                if (liveround != 4)
                {
                    liveround++;
                    LifeRoundCount.Text = liveround.ToString();
                }
            }
            else
            {
                if (blank != 4)
                {
                    blank++;
                    BlankCount.Text = blank.ToString();
                }
            }
        }
        private void Remove_Patron(object sender, EventArgs e)
        {
            Button cur_button = (Button)sender; // Инициализируем кнопку минуса
            if (cur_button.Name == LifeRoundMinusButton.Name)
            {
                if (liveround != 0)
                {
                    liveround--;
                    LifeRoundCount.Text = liveround.ToString();
                }
            }
            else
            {
                if (blank != 0)
                {
                    blank--;
                    BlankCount.Text = blank.ToString();
                }
            }
        }
        private void Change_MAX_HP(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender; // Инициализуем радиокнопку
            max_hp = int.Parse(radioButton.Text);
            foreach (RadioButton button in YouHp.Controls)
            {
                if (max_hp.ToString() == button.Text) button.Checked = true;
                if (max_hp < int.Parse(button.Text)) button.Visible = false;
                else button.Visible = true;
            }
            foreach (RadioButton button in DealerHp.Controls)
            {
                if (max_hp.ToString() == button.Text) button.Checked = true;
                if (max_hp < int.Parse(button.Text)) button.Visible = false;
                else button.Visible= true;
            }
        }
        private void Change_Player_HP(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender; // Инициализуем радиокнопку
            if (YouHp.Controls.ContainsKey(radioButton.Name))
            {
                you_hp = int.Parse(radioButton.Text);
            }
            else
            {
                dealer_hp = int.Parse(radioButton.Text);
            }
        }
        private void Calculate_Click(object sender, EventArgs e)
        {
            Result.Items.Clear();
            Solution calculate = new Solution(max_hp);
            calculate.MeCalculate(you_hp, dealer_hp, liveround, blank, 1, "", you_items, dealer_items, new string[liveround + blank], new string[liveround + blank], true);
            Choice[] result = calculate.PrintResult();
            for (int i = 0; i < result.Length; i++)
            {
                Result.Items.Add(result[i].path);
                Result.Items.Add("lose/win/ev: "+Prunning(result[i].lose.ToString())+"/"+
                    Prunning(result[i].win.ToString()) + "/" + Prunning(result[i].ev.ToString()));
            }
        }

        private static string Prunning(string s)
        {
            string result = "";
            if (Math.Abs(double.Parse(s)) < 0.001) return "0";
            for (int i = 0; i < Math.Min(s.Length, 5); i++)
            {
                result += s[i];
            }
            return result;
        }
    }
}
