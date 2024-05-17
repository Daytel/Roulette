using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette
{
    internal class Simulation
    {
        public static int UseAdrenalin(int count, int used_items) // Метод для посчёта использованного Адреналина
        {
            if (count <= used_items) return 0;
            return count - used_items;
        }

        public static double KillME(int YHP, int LR, int BL, Dictionary<string, int> MI, Dictionary<string, int> HI) // Мой ход с целью ВЫЖИТЬ В РАУНДЕ
        {
            double ev = 0; // Вероятность умереть
            bool end = false; // Флаг для начала подсчёта вероятности
            if (LR == 0) return 0;
            if (YHP == 1)
            {
                if (BL == 0 && MI["Beer"] + MI["Inverter"] + Math.Min(MI["Adrenalin"], HI["Inverter"] + HI["Beer"]) < LR - 1) return 1;
                Solve(1, YHP, LR, BL, MI, HI);
                return ev;
            }
            else if (YHP == 2)
            {
                if (BL == 0)
                { // НЕЛЬЗЯ: боевой + пила, наручники + 2 боевых
                    if (MI["Beer"] + MI["Inverter"] + Math.Min(MI["Adrenalin"], HI["Inverter"] + HI["Beer"]) < LR - 1 && HI["Saw"] != 0) return 1;
                    if (MI["Beer"] + MI["Inverter"] + Math.Min(MI["Adrenalin"], HI["Inverter"] + HI["Beer"]) < LR - 2 && HI["Handcuffs"] != 0) return 1;
                    return 0;
                }
                Solve(1, YHP, LR, BL, MI, HI);
                return ev;
            }
            else return 0;

            void Solve(double chance, int yhp, int lr, int bl, Dictionary<string, int> mi, Dictionary<string, int> hi) // Решает ситуации для LR = 1+, BL = 1+
            {
                // Надо проверить удовлетворяют ли все условия выхода и для 2 HP
                if (ev == 1) return; // Нет выхода
                if (lr == 0) return; // Всё в себя
                if (lr + bl == 1) return; // Последний боевой в него
                if ((lr + bl) == 2 && mi["Handcuffs"] + Math.Min(mi["Adrenalin"], hi["Handcuffs"]) != 0) return; // Надеваем наручники и оба в него

                if (end) // Ход завершён, подсчёт итогов
                {
                    ev += CanKill(yhp, lr, bl, mi, hi); return;
                }

                if (yhp == 1)
                {
                    // 1. Если нет холостых мы их инвертируем
                    if (bl == 0)
                    {
                        if (mi["Beer"] + mi["Inverter"] + Math.Min(mi["Adrenalin"], hi["Inverter"] + hi["Beer"]) < lr - 1)
                        {
                            ev = 1; return;
                        }
                    }
                    else
                    {
                        // 2. Стараемся сократить число патронов используя пиво
                        if (mi["Beer"] != 0)
                        {
                            mi["Beer"]--;
                            lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем боевой
                            lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем холостой
                        }
                        if (Math.Min(mi["Adrenalin"], hi["Beer"]) != 0)
                        {
                            mi["Adrenalin"]--; hi["Beer"]--;
                            lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем боевой
                            lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем холостой
                        }
                        if (mi["Magnifer"] != 0)
                        {
                            mi["Magnifer"]--;
                            bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Нашли холостой - выплёвываем
                            bl++;
                            if (lr == 1 && (mi["Inverter"] + Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0) ||
                                mi["Handcuffs"] + Math.Min(mi["Adrenalin"], hi["Handcuffs"]) != 0) return; // Расходуем последний боевой и выплёвываем всё
                            if (hi["Inverter"] != 0)
                            {
                                hi["Inverter"]--; lr--;
                                Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi);
                            }
                            if (Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0)
                            {
                                mi["Adrenalin"]--; hi["Inverter"]--;
                                Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi);
                            }

                            end = true; // Завершаем ход
                            if (mi["Handcuffs"] + +Math.Min(mi["Adrenalin"], hi["Handcuffs"]) != 0)
                            {
                                // Надеваем наручники и делаем 2 выстрела в него
                                lr--; // Стреляем текущим боевым
                                if (bl > lr)
                                {
                                    // Будем пытаться инвертировать 2-ой патрон, чтобы сделать меньше боевых
                                    if (hi["Adrenalin"] != 0)
                                    {
                                        // Смысла воровать инвертор нет
                                        if (mi["Inverter"] + Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0)
                                        {
                                            mi["Inverter"]--;
                                            lr--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                    }
                                    else
                                    {
                                        if (Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0)
                                        {
                                            // Воруем инвертор и стреляем в него
                                            mi["Adrenalin"]--; hi["Inverter"]--;
                                            lr--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                        else if (mi["Inverter"] != 0)
                                        {
                                            // Используем инвертор и стреляем в него
                                            mi["Inverter"]--;
                                            lr--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                        else
                                        {
                                            // Просто стреляем
                                            lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                    }
                                }
                                else
                                {
                                    lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                    lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                }
                            }
                            else
                            {
                                // Просто стреляем в него
                                lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                            }
                        }
                    }
                }

                else // У нас 2 HP => можем жертвовать своим HP
                {
                    // 1. Если нет холостых мы их инвертируем
                    if (bl == 0)
                    {
                        if (mi["Beer"] + mi["Inverter"] + Math.Min(mi["Adrenalin"], hi["Inverter"] + hi["Beer"]) < lr - 1)
                        {
                            ev = 1; return;
                        }
                    }
                    else
                    {
                        // 2. Стараемся сократить число патронов используя пиво
                        if (mi["Beer"] != 0)
                        {
                            mi["Beer"]--;
                            lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем боевой
                            lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем холостой
                        }
                        if (Math.Min(mi["Adrenalin"], hi["Beer"]) != 0)
                        {
                            mi["Adrenalin"]--; hi["Beer"]--;
                            lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем боевой
                            lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Выплёвываем холостой
                        }
                        if (mi["Magnifer"] != 0)
                        {
                            mi["Magnifer"]--;
                            bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Нашли холостой - выплёвываем
                            bl++;
                            if (lr == 1 && (mi["Inverter"] + Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0) ||
                                mi["Handcuffs"] + Math.Min(mi["Adrenalin"], hi["Handcuffs"]) != 0) return; // Расходуем последний боевой и выплёвываем всё
                            if (hi["Inverter"] != 0)
                            {
                                hi["Inverter"]--; lr--;
                                Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi);
                            }
                            if (Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0)
                            {
                                mi["Adrenalin"]--; hi["Inverter"]--;
                                Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi);
                            }

                            end = true; // Завершаем ход
                            if (mi["Handcuffs"] + +Math.Min(mi["Adrenalin"], hi["Handcuffs"]) != 0)
                            {
                                // Надеваем наручники и делаем 2 выстрела в него
                                lr--; // Стреляем текущим боевым
                                if (bl > lr)
                                {
                                    // Будем пытаться инвертировать 2-ой патрон, чтобы сделать меньше боевых
                                    if (hi["Adrenalin"] != 0)
                                    {
                                        // Смысла воровать инвертор нет
                                        if (mi["Inverter"] + Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0)
                                        {
                                            mi["Inverter"]--;
                                            lr--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                    }
                                    else
                                    {
                                        if (Math.Min(mi["Adrenalin"], hi["Inverter"]) != 0)
                                        {
                                            // Воруем инвертор и стреляем в него
                                            mi["Adrenalin"]--; hi["Inverter"]--;
                                            lr--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                        else if (mi["Inverter"] != 0)
                                        {
                                            // Используем инвертор и стреляем в него
                                            mi["Inverter"]--;
                                            lr--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                        else
                                        {
                                            // Просто стреляем
                                            lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                            lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                        }
                                    }
                                }
                                else
                                {
                                    lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                                    lr++; bl--; Solve(chance * (bl / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем холостым
                                }
                            }
                            else
                            {
                                // Просто стреляем в него
                                lr--; Solve(chance * (lr / (lr + bl)), yhp, lr, bl, mi, hi); // Стреляем боевым
                            }
                        }
                    }
                }
            }
        }

        public static double CanKill(int DHP, int LR, int BL, Dictionary<string, int> MI, Dictionary<string, int> HI) // ход диллера, шанс убить меня
        {
            double[] result = new double[2];
            if (DHP == 1)
            {
                if (BL == 0) return 1;
                else if (LR == 0)
                {
                    if (MI["Inverter"] != 0 || (MI["Adrenalin"] != 0 && HI["Inverter"] != 0)) return 1;
                    else return 0;
                }
                else if (
                    (BL == 1 && LR == 1 && (MI["Magnifer"] != 0 || MI["Telephone"] != 0)) ||
                    (MI["Magnifer"] != 0 && MI["Inverter"] != 0) ||
                    (MI["Magnifer"] != 0 && MI["Adrenalin"] != 0 && HI["Inverter"] != 0) ||
                    (MI["Inverter"] != 0 && MI["Adrenalin"] != 0 && HI["Magnifer"] != 0) ||
                    (MI["Adrenalin"] > 1 && HI["Inverter"] != 0 && HI["Magnifer"] != 0) ||
                    (BL == 1 && MI["Handcuffs"] != 0) ||
                    (MI["Magnifer"] + MI["Telephone"] >= BL) ||
                    (MI["Handcuffs"] != 0 && (MI["Magnifer"] + MI["Telephone"] >= BL + 1))
                    ) return 1;
                else
                {
                    Solve();
                    return result.Max();
                }
            }
            return 0;

            void Solve()
            {
                // 1. Пробуем убить сразу

            }
        }
    }
}
