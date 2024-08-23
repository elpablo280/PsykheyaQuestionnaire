﻿using Psyche.Interfaces;
using Psyche.Models;

namespace Psyche.Handlers
{
    public class SR45Handler : IHandler
    {
        private readonly List<int> Answers = new();

        public SR45Handler(List<int> answers)
        {
            Answers = answers;
        }

        //public IEnumerable<Sr45>? GetResultTab()
        //{
        //    IEnumerable<Sr45>? result = null;

        //    using (DBContext context = new())
        //    {
        //        context.Database.EnsureCreated();

        //        //context.Sr45s.Load();

        //        result = context.Sr45s;
        //    }

        //    return result;
        //}

        public string GetResult()
        {
            string result = $"Тест СР-45.{Environment.NewLine}";
            int resultL = 0;
            int resultSr = 0;

            for (int i = 0; i < Answers.Count; i++)           // порядковые номера столбцов с ответами в таблице
            {
                if (i == 10 ||
                    i == 11 ||
                    i == 17 ||
                    i == 20 ||
                    i == 22 ||
                    i == 24 ||
                    i == 28 ||
                    i == 33 ||
                    i == 38 ||
                    i != 41)
                {
                    resultL += Answers[i];
                }
                else
                {
                    resultSr += Answers[i];
                }
            }

            if (resultL < 6)
            {
                result += $"Низкий показатель по шкале лжи. Полученные данные достоверны.{Environment.NewLine}";
            }
            else
            {
                result += $"Высокий показатель по шкале лжи. Полученные данные недостоверны.{Environment.NewLine}";
            }

            double resultSrCaclulated = resultSr / 35.0;

            if (resultSrCaclulated >= 0 && resultSrCaclulated <= 0.23)
            {
                result += "Низкий уровень склонности к суицидальным реакциям";
            }
            else if (resultSrCaclulated >= 0.24 && resultSrCaclulated <= 0.38)
            {
                result += "Суицидальная реакция может возникнуть только на фоне длительной психической травматизации и при реактивных состояниях психики.";
            }
            else if (resultSrCaclulated >= 0.39 && resultSrCaclulated <= 0.59)
            {
                result += "«Потенциал» склонности к суицидальным реакциям не отличается высокой устойчивостью.";
            }
            else if (resultSrCaclulated >= 0.6 && resultSrCaclulated <= 0.74)
            {
                result += "Группа суицидального риска с высоким уровнем проявления склонности к суицидальным реакциям (при нарушениях адаптации возможна суицидальная попытка или реализация саморазрушающего поведения).";
            }
            else if (resultSrCaclulated >= 0.75 && resultSrCaclulated <= 1)
            {
                result += "Группа суицидального риска с очень высоким уровнем проявления склонности к суицидальным реакциям (ситуация внутреннего и внешнего конфликта, нуждаются в медико-психологической помощи).";
            }
            else
            {
                throw new Exception("Неправильный результат");
            }

            //try
            //{
            //    using (var connection = new SqliteConnection(Config.ConnectionStrings.UsersDB))
            //    {
            //        connection.Open();

            //        string FIO = $"{CurrentUser.Name}";

            //        // todo
            //        SqliteCommand commandCheck = new()
            //        {
            //            Connection = connection,
            //            CommandText = $"SELECT * FROM SR45 WHERE UserName='{FIO}' AND UserPlatoon='{CurrentUser.Platoon}'",
            //        };
            //        using (SqliteDataReader reader = commandCheck.ExecuteReader())
            //        {
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())           // todo костыль
            //                {
            //                    for (int i = 5; i < 50; i++)           // порядковые номера столбцов с ответами в таблице
            //                    {
            //                        if (i == 14 ||
            //                            i == 15 ||
            //                            i == 21 ||
            //                            i == 24 ||
            //                            i == 26 ||
            //                            i == 28 ||
            //                            i == 32 ||
            //                            i == 37 ||
            //                            i == 42 ||
            //                            i == 45)
            //                        {
            //                            resultL += Convert.ToInt32(reader.GetValue(i));
            //                        }
            //                        else
            //                        {
            //                            resultSr += Convert.ToInt32(reader.GetValue(i));
            //                        }
            //                    }
            //                    break;
            //                }
            //                if (resultL < 6)
            //                {
            //                    result += $"Низкий показатель по шкале лжи. Полученные данные достоверны.{Environment.NewLine}";
            //                }
            //                else
            //                {
            //                    result += $"Высокий показатель по шкале лжи. Полученные данные недостоверны.{Environment.NewLine}";
            //                }

            //                double resultSrCaclulated = resultSr / 35.0;

            //                if (resultSrCaclulated >= 0.01f && resultSrCaclulated <= 0.23f)
            //                {
            //                    result += "Низкий уровень склонности к суицидальным реакциям";
            //                }
            //                else if (resultSrCaclulated >= 0.24f && resultSrCaclulated <= 0.38f)
            //                {
            //                    result += "Суицидальная реакция может возникнуть только на фоне длительной психической травматизации и при реактивных состояниях психики.";
            //                }
            //                else if (resultSrCaclulated >= 0.39f && resultSrCaclulated <= 0.59f)
            //                {
            //                    result += "«Потенциал» склонности к суицидальным реакциям не отличается высокой устойчивостью.";
            //                }
            //                else if (resultSrCaclulated >= 0.6f && resultSrCaclulated <= 0.74f)
            //                {
            //                    result += "Группа суицидального риска с высоким уровнем проявления склонности к суицидальным реакциям (при нарушениях адаптации возможна суицидальная попытка или реализация саморазрушающего поведения).";
            //                }
            //                else if (resultSrCaclulated >= 0.75f && resultSrCaclulated <= 1f)
            //                {
            //                    result += "Группа суицидального риска с очень высоким уровнем проявления склонности к суицидальным реакциям (ситуация внутреннего и внешнего конфликта, нуждаются в медико-психологической помощи).";
            //                }
            //                else
            //                {
            //                    throw new Exception("Неправильный результат");
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            return result;
        }
    }
}
