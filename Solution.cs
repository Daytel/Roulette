using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Resources.ResXFileRef;

namespace Roulette
{
    class Choice // Класс ответ
    {
        public string path; // Path of action
        public double ev; // damage in the round (EV > 0 - dealer took more damage, else me)
        public double win; // chance win
        public double lose; // chance lose

        public Choice(string path, double ev, double win, double lose)
        {
            this.path = path;
            this.ev = ev;
            this.win = win;
            this.lose = lose;
        }
    }

    internal class Solution
    {
        List<string> choice; // Путь выбора
        List<double> ev, win, lose; // Числинные коэффициенты
        int max_hp; // Максимальное здоровье в раунде

        public Solution(int max_hp)
        {
            choice = new List<string> { };
            ev = new List<double> { };
            win = new List<double> { };
            lose = new List<double> { };
            this.max_hp = max_hp;
        }

        public void MeCalculate(int YHP, int DHP, int LR, int BL, double ch, string path, Dictionary<string, int> MI, Dictionary<string, int> HI, string[] BM, string[] BD, bool FM)
        {
            // Условия завершения рекурсии
            // 1. Кто-то умер
            if (YHP < 1) lose[choice.IndexOf(path)] += ch;
            else if (DHP < 1) win[choice.IndexOf(path)] += ch;

            // 2. Кончились патроны
            else if (LR + BL != 0)
            {
                // Иначе считаем стратегию
                // Если первый ход - создаём новую ветвь
                if (FM && path == "")
                {
                    choice.Add(path);
                    ev.Add(0);
                    win.Add(0);
                    lose.Add(0);
                }
                // 1. Процесс хила

                // Использование "Лекарства" экстренно
                if (MI["Cigarettes"] == 0 && (MI["Adrenalin"] == 0 && HI["Cigarettes"] == 0) &&
                    Simulation.KillME(YHP, LR, BL, MI, HI) > 0.5 && Simulation.CanKill(DHP, LR, BL, MI, HI) < 0.5) // Нам выгодней есть таблетку
                {
                    if (FM)
                    {
                        choice[-1] += "Medicine ";
                        path += "Medicine ";
                    }
                    MI["Medicine"]--;
                    MeCalculate(YHP + 2, DHP, LR, BL, ch * 0.5, path, MI, HI, BM, BD, FM);
                    MeCalculate(YHP - 1, DHP, LR, BL, ch * 0.5, path, MI, HI, BM, BD, FM);
                }

                // Обычное использование сигарет и таблеток
                if (YHP != max_hp)
                {
                    // Таблетки
                    if (MI["Medicine"] != 0 && YHP == 2 && max_hp == 4)
                    {
                        if (FM)
                        {
                            choice[-1] += "Medicine ";
                            path += "Medicine ";
                        }
                        MI["Medicine "]--;
                        MeCalculate(YHP + 2, DHP, LR, BL, ch * 0.5, path, MI, HI, BM, BD, FM);
                        MeCalculate(YHP - 1, DHP, LR, BL, ch * 0.5, path, MI, HI, BM, BD, FM);
                    }
                    // Сигареты
                    else if (MI["Cigarettes"] != 0)
                    {
                        if (FM)
                        {
                            choice[-1] += "Cigarettes ";
                            path += "Cigarettes ";
                        }
                        MI["Cigarettes"]--;
                        MeCalculate(YHP + 1, DHP, LR, BL, ch, path, MI, HI, BM, BD, FM);
                    }
                }

                // Использование патронов
                if (LR == 0) // Остались холостые
                {
                    // Проверка на условие 100% победы
                    if (DHP == 1)
                    {
                        if (MI["Inverter"] != 0 || (MI["Adrenalin"] != 0 && HI["Inverter"] != 0)){
                            if (FM)
                            {
                                choice[-1] += "Inverter Dealer";
                                path += "Inverter Dealer";
                            }
                            ev[-1] += ch;
                            DealerCalculate(YHP, DHP - 1, LR, BL - 1, ch, path, MI, HI, BM, BD, false);
                        }
                        else
                        {
                            if (FM)
                            {
                                choice[-1] += "Me";
                                path += "Me";
                            }
                            MeCalculate(YHP, DHP, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                    }
                    else if (DHP == 2)
                    {
                        // Наручники + 2 Инвертора
                        if (BL >= 2 &&(MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Inverter Dealer Inverter Dealer";
                                path += "Handcuffs Inverter Dealer Inverter Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR, BL - 2, ch, path, MI, HI, BM, BD, false);
                        }
                        // Пила + Инвертер
                        else if (MI["Saw"] + Math.Min(MI["Adrenalin"], HI["Saw"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) != 0)
                        {
                            if (FM)
                            {
                                choice[-1] += "Inverter Saw Dealer";
                                path += "Inverter Saw Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR, BL - 1, ch, path, MI, HI, BM, BD, false);
                        }
                        // На последний патрон Инвертер
                        else if (BL == 1 && (MI["Inverter"] != 0 || (MI["Adrenalin"] != 0 && HI["Inverter"] != 0)))
                        {
                            if (FM)
                            {
                                choice[-1] += "Inverter Dealer";
                                path += "Inverter Dealer";
                            }
                            ev[-1] += ch;
                            DealerCalculate(YHP, DHP - 1, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        else
                        {
                            if (FM)
                            {
                                choice[-1] += "Me... ";
                                path += "Me... ";
                            }
                            MeCalculate(YHP, DHP, LR, BL - 1, ch, path, MI, HI, BM, BD, FM);
                        }
                    }
                    else if (DHP == 3)
                    {
                        // Наручники + Пила + 2 Инвертора
                        if (BL >=2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Saw"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Handcuffs"]), HI["Saw"]) != 0 &&
                            MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(2, MI["Handcuffs"] + MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Inverter Saw Dealer Inverter Dealer";
                                path += "Handcuffs Inverter Saw Dealer Inverter Dealer";
                            }
                            ev[-1] += ch * 3;
                            DealerCalculate(YHP, DHP - 3, LR, BL - 2, ch, path, MI, HI, BM, BD, false);
                        }
                        // на 2 патрона Наручники + 2 инвертора
                        else if (BL == 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Inverter Dealer Inverter Dealer";
                                path += "Handcuffs Inverter Dealer Inverter Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        // на последний патрон Пила + Инвертор
                        else if (BL == 1 && (MI["Saw"] + Math.Min(MI["Adrenalin"], HI["Saw"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) != 0))
                        {
                            if (FM)
                            {
                                choice[-1] += "Inverter Saw Dealer";
                                path += "Inverter Saw Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        // на последний Инвертор
                        else if (BL == 1 && (MI["Inverter"] != 0 || (MI["Adrenalin"] != 0 && HI["Inverter"] != 0)))
                        {
                            if (FM)
                            {
                                choice[-1] += "Inverter Dealer";
                                path += "Inverter Dealer";
                            }
                            ev[-1] += ch;
                            DealerCalculate(YHP, DHP - 1, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        else
                        {
                            if (FM)
                            {
                                choice[-1] += "Me... ";
                                path += "Me... ";
                            }
                            MeCalculate(YHP, DHP, LR, BL - 1, ch, path, MI, HI, BM, BD, FM);
                        }
                    }
                    else // DHP == 4
                    {
                        // Наручники + 2 Пилы + 2 Инвертора
                        if (BL >= 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Saw"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Handcuffs"]), HI["Saw"]) > 1 &&
                            MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(3, MI["Handcuffs"] + MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Inverter Saw Dealer Inverter Saw Dealer";
                                path += "Handcuffs Inverter Saw Dealer Inverter Saw Dealer";
                            }
                            ev[-1] += ch * 4;
                            DealerCalculate(YHP, DHP - 4, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        // на 2 патрона Наручники + Пила + 2 Инвертора
                        else if (BL == 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 &&
                            MI["Saw"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Handcuffs"]), HI["Saw"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(2, MI["Handcuffs"] + MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Inverter Saw Dealer Inverter Dealer";
                                path += "Handcuffs Inverter Saw Dealer Inverter Dealer";
                            }
                            ev[-1] += ch * 3;
                            DealerCalculate(YHP, DHP - 3, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        // на 2 патрона Наручники + 2 инвертора
                        else if (BL == 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Inverter Dealer Inverter Dealer";
                                path += "Handcuffs Inverter Dealer Inverter Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        // на последний патрон Пила + Инвертор
                        else if (BL == 1 && (MI["Saw"] + Math.Min(MI["Adrenalin"], HI["Saw"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) != 0))
                        {
                            if (FM)
                            {
                                choice[-1] += "Inverter Saw Dealer";
                                path += "Inverter Saw Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        // на последний Инвертор
                        else if (BL == 1 && (MI["Inverter"] != 0 || (MI["Adrenalin"] != 0 && HI["Inverter"] != 0)))
                        {
                            if (FM)
                            {
                                choice[-1] += "Inverter Dealer";
                                path += "Inverter Dealer";
                            }
                            ev[-1] += ch;
                            DealerCalculate(YHP, DHP - 1, LR, 0, ch, path, MI, HI, BM, BD, false);
                        }
                        else
                        {
                            if (FM)
                            {
                                choice[-1] += "Me... ";
                                path += "Me... ";
                            }
                            MeCalculate(YHP, DHP, LR, BL - 1, ch, path, MI, HI, BM, BD, FM);
                        }
                    }                  
                }
                else if (BL == 0) // Остались боевые
                {
                    // Проверка на условие 100% победы
                    if (DHP == 1)
                    {
                        if (FM)
                        {
                            choice[-1] += "Dealer";
                            path += "Dealer";
                        }
                        ev[-1] += ch;
                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                    }
                    else if (DHP == 2)
                    {
                        // 2 патрона + Наручники
                        if (LR >= 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 ))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Dealer Dealer";
                                path += "Handcuffs Dealer Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // Пила
                        else if (MI["Saw"] + Math.Min(MI["Adrenalin"], HI["Saw"]) != 0)
                        {
                            if (FM)
                            {
                                choice[-1] += "Saw Dealer";
                                path += "Saw Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // На последний патрон - просто бьём
                        else if (LR == 1)
                        {
                            if (FM)
                            {
                                choice[-1] += "Dealer";
                                path += "Dealer";
                            }
                            ev[-1] += ch;
                            DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        else // Тратим предметы, пытаясь пропустить раунд
                        {
                            if (Simulation.CanKill(YHP, LR - 1, BL, MI, HI) == 0) // Если диллер не убивает на своём ходе
                            {
                                // По EV мы впереди: 2 в него 1 в себя
                                if (LR == 3 && HI["Handcuffs"] == 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Dealer";
                                        path += "Dealer";
                                    }
                                    ev[-1] += ch;
                                    DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                // Иначе скипаем патрон для норм ситуации (не тратим адреналин)
                                else if (MI["Beer"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Beer ";
                                        path += "Beer ";
                                    }
                                    MI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (MI["Inverter"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Inverter ";
                                        path += "Inverter ";
                                    }
                                    MI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                // Иначе просто стреляем
                                else
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Dealer";
                                        path += "Dealer";
                                    }
                                    ev[-1] += ch;
                                    DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                }
                            }
                            else // Пытаемся выжить
                            {
                                if (MI["Beer"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Beer ";
                                        path += "Beer ";
                                    }
                                    MI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (MI["Inverter"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Inverter ";
                                        path += "Inverter ";
                                    }
                                    MI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (HI["Inverter"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Inverter ";
                                        path += "Inverter ";
                                    }
                                    MI["Adrenalin"]--; HI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (HI["Beer"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Beer ";
                                        path += "Beer ";
                                    }
                                    MI["Adrenalin"]--; HI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else
                                {
                                    // Увы и ах
                                    if (FM)
                                    {
                                        choice[-1] += "Dealer";
                                        path += "Dealer";
                                    }
                                    ev[-1] += ch;
                                    DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                }
                            }
                        }
                    }
                    else if (DHP == 3)
                    {
                        // Наручники + Пила
                        if (LR >= 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Saw"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Handcuffs"]), HI["Saw"]) != 0))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Saw Dealer Dealer";
                                path += "Handcuffs Saw Dealer Dealer";
                            }
                            ev[-1] += ch * 3;
                            DealerCalculate(YHP, DHP - 3, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // на 2 последних патрона Наручники
                        else if (LR == 2 && MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0)
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Dealer Dealer";
                                path += "Handcuffs Dealer Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // на последний патрон Пила
                        else if (LR == 1 && (MI["Saw"] + Math.Min(MI["Adrenalin"], HI["Saw"]) != 0))
                        {
                            if (FM)
                            {
                                choice[-1] += "Saw Dealer";
                                path += "Saw Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        else // Тратим предметы для скипа
                        {
                            if (Simulation.CanKill(YHP, LR - 1, BL, MI, HI) == 0) // Если диллер не убивает на своём ходе
                            {
                                // Наручники сразу используем т.к. их использование всегда выгодно
                                if (HI["Handcuffs"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Dealer Dealer";
                                        path += "Handcuffs Dealer Dealer";
                                    }
                                    HI["Handcuffs"]--; MI["Adrenalin"]--;
                                    ev[-1] += ch * 2;
                                    DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                else if (HI["Handcuffs"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Dealer Dealer";
                                        path += "Handcuffs Dealer Dealer";
                                    }
                                    MI["Handcuffs"]--;
                                    ev[-1] += ch * 2;
                                    DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                // Используем пилу или просто бьём когда патронов нечётно
                                else if (LR == 3 && HI["Handcuffs"] == 0)
                                {
                                    // Используем пилу
                                    if (HI["Saw"] != 0 && MI["Adrenalin"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        HI["Saw"]--; MI["Adrenalin"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else if (MI["Saw"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        MI["Saw"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else // Просто бьём
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Dealer";
                                            path += "Dealer";
                                        }
                                        ev[-1] += ch;
                                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                }
                                // Иначе пробуем скипнуть патрон
                                else
                                {
                                    if (MI["Beer"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Beer ";
                                            path += "Beer ";
                                        }
                                        MI["Beer"]--;
                                        MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                    }
                                    else if (MI["Inverter"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Inverter ";
                                            path += "Inverter ";
                                        }
                                        MI["Inverter"]--;
                                        MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                    }
                                    // Если не получилось используем пилу/стреляем
                                    else if (HI["Saw"] != 0 && MI["Adrenalin"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        HI["Saw"]--; MI["Adrenalin"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else if (MI["Saw"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        MI["Saw"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else // Просто бьём
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Dealer";
                                            path += "Dealer";
                                        }
                                        ev[-1] += ch;
                                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);

                                    }
                                }
                            }
                            else // Пробуем выжить
                            {
                                if (MI["Beer"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Beer ";
                                        path += "Beer ";
                                    }
                                    MI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (MI["Inverter"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Inverter ";
                                        path += "Inverter ";
                                    }
                                    MI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (HI["Inverter"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Inverter ";
                                        path += "Inverter ";
                                    }
                                    MI["Adrenalin"]--; HI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (HI["Beer"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Beer ";
                                        path += "Beer ";
                                    }
                                    MI["Adrenalin"]--; HI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Dealer";
                                        path += "Dealer";
                                    }
                                    // Надеемся на лучшее
                                    ev[-1] += ch;
                                    DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                }
                            }
                        }
                    }
                    else // DHP == 4
                    {
                        // Наручники + 2 Пилы
                        if (LR >= 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Saw"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Handcuffs"]), HI["Saw"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Saw Dealer Saw Dealer";
                                path += "Handcuffs Saw Dealer Saw Dealer";
                            }
                            ev[-1] += ch * 4;
                            DealerCalculate(YHP, DHP - 4, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // на 2 патрона Наручники + Пила
                        else if (BL == 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Saw"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Handcuffs"]), HI["Saw"]) != 0))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Saw Dealer Dealer";
                                path += "Handcuffs Saw Dealer Dealer";
                            }
                            ev[-1] += ch * 3;
                            DealerCalculate(YHP, DHP - 3, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // на 2 патрона Наручники
                        else if (BL == 2 && (MI["Handcuffs"] + Math.Min(MI["Adrenalin"], HI["Handcuffs"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) > 1))
                        {
                            if (FM)
                            {
                                choice[-1] += "Handcuffs Dealer Dealer";
                                path += "Handcuffs Dealer Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        // на последний патрон Пила
                        else if (BL == 1 && (MI["Saw"] + Math.Min(MI["Adrenalin"], HI["Saw"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Saw"]), HI["Inverter"]) != 0))
                        {
                            if (FM)
                            {
                                choice[-1] += "Saw Dealer";
                                path += "Saw Dealer";
                            }
                            ev[-1] += ch * 2;
                            DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        else // Пробуем скипнуть патроны для комбы
                        {
                            if (Simulation.CanKill(YHP, LR - 1, BL, MI, HI) == 0) // Если диллер не убивает на своём ходе
                            {
                                // Используем наручники и пилу поскольку это всегда выгодно
                                if (HI["Handcuffs"] != 0 && HI["Saw"] != 0 && MI["Adrenalin"] > 1)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Saw Dealer Dealer";
                                        path += "Handcuffs Saw Dealer Dealer";
                                    }
                                    HI["Handcuffs"]--; HI["Saw"]--; MI["Adrenalin"] -= 2;
                                    ev[-1] += ch * 3;
                                    DealerCalculate(YHP, DHP - 3, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                else if (HI["Handcuffs"] != 0 && MI["Saw"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Saw Dealer Dealer";
                                        path += "Handcuffs Saw Dealer Dealer";
                                    }
                                    HI["Handcuffs"]--; MI["Saw"]--; MI["Adrenalin"]--;
                                    ev[-1] += ch * 3;
                                    DealerCalculate(YHP, DHP - 3, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                else if (MI["Handcuffs"] != 0 && HI["Saw"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Saw Dealer Dealer";
                                        path += "Handcuffs Saw Dealer Dealer";
                                    }
                                    MI["Handcuffs"]--; HI["Saw"]--; MI["Adrenalin"]--;
                                    ev[-1] += ch * 3;
                                    DealerCalculate(YHP, DHP - 3, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                else if (MI["Handcuffs"] != 0 && MI["Saw"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Saw Dealer Dealer";
                                        path += "Handcuffs Saw Dealer Dealer";
                                    }
                                    MI["Handcuffs"]--; MI["Saw"]--;
                                    ev[-1] += ch * 3;
                                    DealerCalculate(YHP, DHP - 3, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                // Используем наручники
                                else if (HI["Handcuffs"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Dealer Dealer";
                                        path += "Handcuffs Dealer Dealer";
                                    }
                                    HI["Handcuffs"]--; MI["Adrenalin"]--;
                                    ev[-1] += ch * 2;
                                    DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                else if (HI["Handcuffs"] != 0)
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Handcuffs Dealer Dealer";
                                        path += "Handcuffs Dealer Dealer";
                                    }
                                    MI["Handcuffs"]--;
                                    ev[-1] += ch * 2;
                                    DealerCalculate(YHP, DHP - 2, LR - 2, BL, ch, path, MI, HI, BM, BD, false);
                                }
                                // Используем пилу и просто бьём когда патроно нечётно
                                else if (LR == 3 && HI["Handcuffs"] == 0)
                                {
                                    // Используем пилу
                                    if (HI["Saw"] != 0 && MI["Adrenalin"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        HI["Saw"]--; MI["Adrenalin"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else if (MI["Saw"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        MI["Saw"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else // Просто бьём
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Dealer";
                                            path += "Dealer";
                                        }
                                        ev[-1] += ch;
                                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                }
                                else // Пробуем скипнуть патрон
                                {
                                    if (MI["Beer"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Beer ";
                                            path += "Beer ";
                                        }
                                        MI["Beer"]--;
                                        MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                    }
                                    else if (MI["Inverter"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Inverter ";
                                            path += "Inverter ";
                                        }
                                        MI["Inverter"]--;
                                        MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                    }
                                    // Если не получилось используем пилу/стреляем
                                    else if (HI["Saw"] != 0 && MI["Adrenalin"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        HI["Saw"]--; MI["Adrenalin"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else if (MI["Saw"] != 0)
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Saw Dealer";
                                            path += "Saw Dealer";
                                        }
                                        MI["Saw"]--;
                                        ev[-1] += ch * 2;
                                        DealerCalculate(YHP, DHP - 2, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                    }
                                    else // Просто бьём
                                    {
                                        if (FM)
                                        {
                                            choice[-1] += "Dealer";
                                            path += "Dealer";
                                        }
                                        ev[-1] += ch;
                                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);

                                    }
                                }
                            }
                            else // Пробуем скипнуть патрон
                            {
                                if (MI["Beer"] != 0)
                                {
                                    MI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (MI["Inverter"] != 0)
                                {
                                    MI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (HI["Inverter"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    MI["Adrenalin"]--; HI["Inverter"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else if (HI["Beer"] != 0 && MI["Adrenalin"] != 0)
                                {
                                    MI["Adrenalin"]--; HI["Beer"]--;
                                    MeCalculate(YHP, DHP, LR - 1, BL, ch, path, MI, HI, BM, BD, FM);
                                }
                                else
                                {
                                    // Надеемся на лучшее
                                    ev[-1] += ch;
                                    DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                                }
                            }
                        }
                    }
                }
                else // есть и холостые и боевые
                {
                    double lr = LR / (LR + BL);
                    // Проверка на условие 100% победы
                    if (DHP == 1)
                    {
                        // Если знаем текущий патрон
                        if (BM[8 - LR - BL] == "LiveRound")
                        {
                            ev[-1] += ch;
                            DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        else if (BM[8 - LR - BL] == "Blank")
                        {
                            if (MI["Inverter"] + Math.Min(MI["Adrenalin"], HI["Inverter"]) != 0)
                            {
                                ev[-1] += ch;
                                DealerCalculate(YHP, DHP - 1, LR, BL - 1, ch, path, MI, HI, BM, BD, false);
                            }
                            else
                            {
                                MeCalculate(YHP, DHP, LR, BL - 1, ch, path, MI, HI, BM, BD, FM);
                            }
                        }
                        // Лупа + Инвертор
                        else if (MI["Magnifer"] + Math.Min(MI["Adrenalin"], HI["Magnifer"]) != 0 && MI["Inverter"] + Math.Min(MI["Adrenalin"] - Simulation.UseAdrenalin(1, MI["Magnifer"]), HI["Inverter"]) != 0)
                        {
                            if (FM)
                            {
                                choice[-1] += "Magnifer Inverter";
                                path += "Magnifer Inverter";
                            }
                            ev[-1]++;
                            DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch, path, MI, HI, BM, BD, false);
                        }
                        else // Если на 100% не добиваем
                        {
                            if (LR > BL) // Боевых больше холостых
                            {
                                if (MI["Magnifer"] != 0) // Используем лупы
                                {
                                    if (FM)
                                    {
                                        choice[-1] += "Magnifer ";
                                        path += "Magnifer ";
                                    }
                                    MI["Magnifer"]--;
                                    DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch * lr, path + "Dealer", MI, HI, BM, BD, false); // Если нашли боевой
                                    DealerCalculate(YHP, DHP, LR, BL - 1, ch * (1 - lr), path + "Me", MI, HI, BM, BD, false); // Если нашли холостой
                                }
                                else if (MI["Adrenalin"] != 0 && HI["Magnifer"] != 0)
                                {

                                }
                            }
                            else if (LR == BL) // Боевых и холостых поровну
                            {

                            }
                            else // Холостых больше боевых
                            {

                            }
                        }
                    }
                    if (path == "")
                    {
                        // В диллера
                        choice.Add("Dealer");
                        ev.Add(ch * damage);
                        win.Add(0);
                        lose.Add(0);
                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch * damage, choice[choice.Count - 1], MI, HI);
                        DealerCalculate(YHP, DHP, LR, BL - 1, ch * (1 - damage), choice[choice.Count - 1], MI, HI);

                        // В себя
                        choice.Add("You");
                        ev.Add(-(ch * damage));
                        win.Add(0);
                        lose.Add(0);
                        DealerCalculate(YHP - 1, DHP, LR - 1, BL, ch * damage, choice[choice.Count - 1], MI, HI);
                        MeCalculate(YHP, DHP, LR, BL - 1, ch * (1 - damage), choice[choice.Count - 1], MI, HI);
                    }
                    else
                    {
                        // Иначе только в диллера
                        ev[choice.IndexOf(path)] += ch * damage;
                        DealerCalculate(YHP, DHP - 1, LR - 1, BL, ch * damage, path, MI, HI);
                        DealerCalculate(YHP, DHP, LR, BL - 1, ch * (1 - damage), path, MI, HI);
                    }

                    
                }
            }
        }

        public void DealerCalculate(int YHP, int DHP, int LR, int BL, double ch, string path, Dictionary<string, int> MI, Dictionary<string, int> HI, string[] BM, string[] BD, bool FM)
        {
            // Условия завершения рекурсии
            // 1. Кто-то умер
            if (YHP < 1) lose[choice.IndexOf(path)] += ch;
            else if (DHP < 1) win[choice.IndexOf(path)] += ch;

            // 2. Кончились патроны
            else if(LR + BL != 0)
            {
                double damage = (double)LR / (LR + BL); // Шанс боевого патрона
                if (LR > BL || (DHP == 1 && LR != 0)) // боевых больше холостых/1 HP при наличии боевых - в меня
                {
                    ev[choice.IndexOf(path)] -= ch * damage;
                    MeCalculate(YHP - 1, DHP, LR - 1, BL, ch * damage, path, MI, HI);
                    if (BL != 0) MeCalculate(YHP, DHP, LR, BL - 1, ch * (1 - damage), path, MI, HI);
                }
                else // Иначе - в себя
                {
                    ev[choice.IndexOf(path)] += ch * damage;
                    if (LR != 0) MeCalculate(YHP, DHP - 1, LR - 1, BL, ch * damage, path, MI, HI);
                    DealerCalculate(YHP, DHP, LR, BL - 1, ch * (1 - damage), path, MI, HI);
                }
            }
        }

        

        
        static bool SkipRound() {  return false; }


        public Choice[] PrintResult()
        {
            Choice[] result = new Choice[choice.Count];
            for (int i = 0; i < choice.Count; i++)
            {
                result[i] = new Choice(choice[i], ev[i], win[i], lose[i]);
            }
            return SolutionSort(result, 0, result.Length - 1);
        }
        static Choice[] SolutionSort(Choice[] array, int start, int end)
        {
            if (start < end)
            {
                int sup = Partition(array, start, end);
                if (sup > 1) // Есть несортированные элементы слева
                {
                    return SolutionSort(array, start, sup - 1);
                }
                if (sup + 1 < end) // Есть несортированные элементы справа
                {
                    return SolutionSort(array, sup + 1, end);
                }
            }
            return array;
        }
        static int Partition(Choice[] array, int left, int right)
        {
            Choice sup = array[new Random().Next(left, right)];
            while (true)
            {
                // Слева пропускаем элементы у которых: < lose, > win, > ev
                while (array[left].lose < sup.lose) left++;
                while (array[left].win > sup.win) left++;
                while (array[left].ev > sup.ev) left++;

                // Справа пропускаем элементы у которых: > lose, < win, < ev
                while (array[right].lose > sup.lose) right--;
                while (array[right].win < sup.win) right--;
                while (array[right].ev < sup.ev) right--;

                if (left < right)
                {
                    Choice buffer = array[left];
                    array[left] = array[right];
                    array[right] = buffer;
                    left++; right--;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
