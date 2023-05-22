using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnglishTrainer
{
    public class EnglishTrainerContextSeed
    {
        public static async Task SeedAsync(EFContext context, ILogger logger, int retry =0)
        {
            var retryForAvailability = retry;

            try
            {
                if (!await context.IrregularVerbs.AnyAsync())
                {
                    await context.AddRangeAsync(GetPreconfiguredVerbs());

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(context, logger, retryForAvailability);
            }
        }

        private static IEnumerable<IrregularVerb> GetPreconfiguredVerbs()
        {
            return new List<IrregularVerb>
            {
                new IrregularVerb { Infinitive = "be", PastSimple = "was/were", PastParticiple = "been", ShortTranslate = "быть/являться" },
                new IrregularVerb { Infinitive = "beat", PastSimple = "beat", PastParticiple = "beaten", ShortTranslate = "бить/колотить" },
                new IrregularVerb { Infinitive = "become", PastSimple = "became", PastParticiple = "become", ShortTranslate = "становиться" },
                new IrregularVerb { Infinitive = "begin", PastSimple = "began", PastParticiple = "begun", ShortTranslate = "начинать" },
                new IrregularVerb { Infinitive = "bend", PastSimple = "bent", PastParticiple = "bent", ShortTranslate = "гнуть" },
                new IrregularVerb { Infinitive = "bet", PastSimple = "bet", PastParticiple = "bet", ShortTranslate = "держать пари" },
                new IrregularVerb { Infinitive = "bite", PastSimple = "bit", PastParticiple = "bitten", ShortTranslate = "кусать" },
                new IrregularVerb { Infinitive = "blow", PastSimple = "blew", PastParticiple = "blown", ShortTranslate = "дуть, выдыхать" },
                new IrregularVerb { Infinitive = "break", PastSimple = "broke", PastParticiple = "broken", ShortTranslate = "ломать, разбивать, разрушать" },
                new IrregularVerb { Infinitive = "bring", PastSimple = "brought", PastParticiple = "brought", ShortTranslate = "приносить, привозить, доставлять" },
                new IrregularVerb { Infinitive = "build", PastSimple = "build", PastParticiple = "build", ShortTranslate = "строить, сооружать" },
                new IrregularVerb { Infinitive = "buy", PastSimple = "bought", PastParticiple = "bought", ShortTranslate = "покупать, приобретать" },
                new IrregularVerb { Infinitive = "catch", PastSimple = "caught", PastParticiple = "caught", ShortTranslate = "ловить, поймать, схватить" },
                new IrregularVerb { Infinitive = "choose", PastSimple = "chose", PastParticiple = "chosen", ShortTranslate = "выбирать, избирать" },
                new IrregularVerb { Infinitive = "come", PastSimple = "came", PastParticiple = "come", ShortTranslate = "приходить, подходить" },
                new IrregularVerb { Infinitive = "cost", PastSimple = "cost", PastParticiple = "cost", ShortTranslate = "стоить, обходиться" },
                new IrregularVerb { Infinitive = "cut", PastSimple = "cut", PastParticiple = "cut", ShortTranslate = "резать, разрезать" },
                new IrregularVerb { Infinitive = "deal", PastSimple = "dealt", PastParticiple = "dealt", ShortTranslate = "иметь дело, распределять" },
                new IrregularVerb { Infinitive = "dig", PastSimple = "dug", PastParticiple = "dug", ShortTranslate = "копать, рыть" },
                new IrregularVerb { Infinitive = "do", PastSimple = "did", PastParticiple = "done", ShortTranslate = "делать, выполнять" },
                new IrregularVerb { Infinitive = "draw", PastSimple = "drew", PastParticiple = "drawn", ShortTranslate = "рисовать, чертить" },
                new IrregularVerb { Infinitive = "drink", PastSimple = "drank", PastParticiple = "drunk", ShortTranslate = "пить" },
                new IrregularVerb { Infinitive = "drive", PastSimple = "drove", PastParticiple = "driven", ShortTranslate = "ездить, подвозить" },
                new IrregularVerb { Infinitive = "eat", PastSimple = "ate", PastParticiple = "eaten", ShortTranslate = "есть, поглощать, поедать" },
                new IrregularVerb { Infinitive = "fall", PastSimple = "fell", PastParticiple = "fallen", ShortTranslate = "падать" },
                new IrregularVerb { Infinitive = "feed", PastSimple = "fed", PastParticiple = "fed", ShortTranslate = "кормить" },
                new IrregularVerb { Infinitive = "feel", PastSimple = "felt", PastParticiple = "felt", ShortTranslate = "чувствовать, ощущать" },
                new IrregularVerb { Infinitive = "fight", PastSimple = "fought", PastParticiple = "fought", ShortTranslate = "драться, сражаться, воевать" },
                new IrregularVerb { Infinitive = "find", PastSimple = "found", PastParticiple = "found", ShortTranslate = "находить, обнаруживать" },
                new IrregularVerb { Infinitive = "fly", PastSimple = "flew", PastParticiple = "flown", ShortTranslate = "летать" },
                new IrregularVerb { Infinitive = "forget", PastSimple = "forgot", PastParticiple = "forgotten", ShortTranslate = "забывать о (чём-либо)" },
                new IrregularVerb { Infinitive = "forgive", PastSimple = "forgave", PastParticiple = "forgiven", ShortTranslate = "прощать" },
                new IrregularVerb { Infinitive = "freeze", PastSimple = "froze", PastParticiple = "frozen", ShortTranslate = "замерзать, замирать" },
                new IrregularVerb { Infinitive = "get", PastSimple = "got", PastParticiple = "got", ShortTranslate = "получать, добираться" },
                new IrregularVerb { Infinitive = "give", PastSimple = "gave", PastParticiple = "given", ShortTranslate = "дать, подать, дарить" },
                new IrregularVerb { Infinitive = "go", PastSimple = "went", PastParticiple = "gone", ShortTranslate = "идти, двигаться" },
                new IrregularVerb { Infinitive = "grow", PastSimple = "grew", PastParticiple = "grown", ShortTranslate = "расти, вырастать" },
                new IrregularVerb { Infinitive = "hang", PastSimple = "hung", PastParticiple = "hung", ShortTranslate = "вешать, развешивать, висеть" },
                new IrregularVerb { Infinitive = "have", PastSimple = "had", PastParticiple = "had", ShortTranslate = "иметь, обладать" },
                new IrregularVerb { Infinitive = "hear", PastSimple = "heard", PastParticiple = "heard", ShortTranslate = "слышать, услышать" },
                new IrregularVerb { Infinitive = "hide", PastSimple = "hid", PastParticiple = "hidden", ShortTranslate = "прятать, скрывать" },
                new IrregularVerb { Infinitive = "hit", PastSimple = "hit", PastParticiple = "hit", ShortTranslate = "ударять, поражать" },
                new IrregularVerb { Infinitive = "hold", PastSimple = "held", PastParticiple = "held", ShortTranslate = "держать, удерживать, задерживать" },
                new IrregularVerb { Infinitive = "hurt", PastSimple = "hurt", PastParticiple = "hurt", ShortTranslate = "ранить, причинять боль, ушибить" },
                new IrregularVerb { Infinitive = "keep", PastSimple = "kept", PastParticiple = "kept", ShortTranslate = "хранить, сохранять, поддерживать" },
                new IrregularVerb { Infinitive = "know", PastSimple = "knew", PastParticiple = "known", ShortTranslate = "знать, иметь представление" },
                new IrregularVerb { Infinitive = "lay", PastSimple = "laid", PastParticiple = "laid", ShortTranslate = "класть, положить, покрывать" },
                new IrregularVerb { Infinitive = "lead", PastSimple = "led", PastParticiple = "led", ShortTranslate = "вести за собой, сопровождать, руководить" },
                new IrregularVerb { Infinitive = "leave", PastSimple = "left", PastParticiple = "left", ShortTranslate = "покидать, уходить, уезжать, оставлять" },
                new IrregularVerb { Infinitive = "lend", PastSimple = "lent", PastParticiple = "lent", ShortTranslate = "одалживать, давать взаймы (в долг)" },
                new IrregularVerb { Infinitive = "let", PastSimple = "let", PastParticiple = "let", ShortTranslate = "позволять, разрешать" },
                new IrregularVerb { Infinitive = "lie", PastSimple = "lay", PastParticiple = "lain", ShortTranslate = "лежать" },
                new IrregularVerb { Infinitive = "light", PastSimple = "lit", PastParticiple = "lit", ShortTranslate = "зажигать, светиться, освещать" },
                new IrregularVerb { Infinitive = "lose", PastSimple = "lost", PastParticiple = "lost", ShortTranslate = "терять, лишаться, утрачивать" },
                new IrregularVerb { Infinitive = "make", PastSimple = "made", PastParticiple = "made", ShortTranslate = "делать, создавать, изготавливать" },
                new IrregularVerb { Infinitive = "mean", PastSimple = "meant", PastParticiple = "meant", ShortTranslate = "значить, иметь в виду, подразумевать" },
                new IrregularVerb { Infinitive = "meet", PastSimple = "met", PastParticiple = "met", ShortTranslate = "встречать, знакомиться" },
                new IrregularVerb { Infinitive = "pay", PastSimple = "paid", PastParticiple = "paid", ShortTranslate = "платить, оплачивать, рассчитываться" },
                new IrregularVerb { Infinitive = "put", PastSimple = "put", PastParticiple = "put", ShortTranslate = "платить, оплачивать, рассчитываться" },
                new IrregularVerb { Infinitive = "read", PastSimple = "read", PastParticiple = "read", ShortTranslate = "читать, прочитать" },
                new IrregularVerb { Infinitive = "ride", PastSimple = "rode", PastParticiple = "ridden", ShortTranslate = "ехать верхом, кататься" },
                new IrregularVerb { Infinitive = "ring", PastSimple = "rang", PastParticiple = "rung", ShortTranslate = "звенеть, звонить" },
                new IrregularVerb { Infinitive = "rise", PastSimple = "rose", PastParticiple = "risen", ShortTranslate = "восходить, вставать, подниматься" },
                new IrregularVerb { Infinitive = "run", PastSimple = "ran", PastParticiple = "run", ShortTranslate = "бежать, бегать" },
                new IrregularVerb { Infinitive = "say", PastSimple = "said", PastParticiple = "said", ShortTranslate = "говорить, сказать, произносить" },
                new IrregularVerb { Infinitive = "see", PastSimple = "saw", PastParticiple = "seen", ShortTranslate = "видеть" },
                new IrregularVerb { Infinitive = "seek", PastSimple = "sought", PastParticiple = "sought", ShortTranslate = "искать, разыскивать" },
                new IrregularVerb { Infinitive = "sell", PastSimple = "sold", PastParticiple = "sold", ShortTranslate = "продавать, торговать" },
                new IrregularVerb { Infinitive = "send", PastSimple = "sent", PastParticiple = "sent", ShortTranslate = "посылать, отправлять, отсылать" },
                new IrregularVerb { Infinitive = "set", PastSimple = "set", PastParticiple = "set", ShortTranslate = "устанавливать, задавать, назначать" },
                new IrregularVerb { Infinitive = "shake", PastSimple = "shook", PastParticiple = "shaken", ShortTranslate = "трясти, встряхивать" },
                new IrregularVerb { Infinitive = "shine", PastSimple = "shone", PastParticiple = "shone", ShortTranslate = "светить, сиять, озарять" },
                new IrregularVerb { Infinitive = "shoot", PastSimple = "shot", PastParticiple = "shot", ShortTranslate = "стрелять" },
                new IrregularVerb { Infinitive = "show", PastSimple = "showed", PastParticiple = "shown/showed", ShortTranslate = "стрелять" },
                new IrregularVerb { Infinitive = "shut", PastSimple = "shut", PastParticiple = "shut", ShortTranslate = "закрывать, запирать, затворять" },
                new IrregularVerb { Infinitive = "sing", PastSimple = "sang", PastParticiple = "sung", ShortTranslate = "петь, напевать" },
                new IrregularVerb { Infinitive = "sink", PastSimple = "sank", PastParticiple = "sunk", ShortTranslate = "тонуть, погружаться" },
                new IrregularVerb { Infinitive = "sit", PastSimple = "sat", PastParticiple = "sat", ShortTranslate = "сидеть, садиться" },
                new IrregularVerb { Infinitive = "sleep", PastSimple = "slept", PastParticiple = "slept", ShortTranslate = "спать" },
                new IrregularVerb { Infinitive = "speak", PastSimple = "spoke", PastParticiple = "spoken", ShortTranslate = "говорить, разговаривать, высказываться" },
                new IrregularVerb { Infinitive = "spend", PastSimple = "spent", PastParticiple = "spent", ShortTranslate = "тратить, расходовать, проводить (время)" },
                new IrregularVerb { Infinitive = "stand", PastSimple = "stood", PastParticiple = "stood", ShortTranslate = "стоять" },
                new IrregularVerb { Infinitive = "steal", PastSimple = "stole", PastParticiple = "stolen", ShortTranslate = "воровать, красть" },
                new IrregularVerb { Infinitive = "stick", PastSimple = "stuck", PastParticiple = "stuck", ShortTranslate = "втыкать, приклеивать" },
                new IrregularVerb { Infinitive = "strike", PastSimple = "struck", PastParticiple = "struck/stricken", ShortTranslate = "ударять, бить, поражать" },
                new IrregularVerb { Infinitive = "swear", PastSimple = "swore", PastParticiple = "sworn", ShortTranslate = "клясться, присягать" },
                new IrregularVerb { Infinitive = "sweep", PastSimple = "swept", PastParticiple = "swept", ShortTranslate = "мести, подметать, смахивать" },
                new IrregularVerb { Infinitive = "swim", PastSimple = "swam", PastParticiple = "swum", ShortTranslate = "плавать, плыть" },
                new IrregularVerb { Infinitive = "swing", PastSimple = "swung", PastParticiple = "swung", ShortTranslate = "качаться, вертеться" },
                new IrregularVerb { Infinitive = "take", PastSimple = "took", PastParticiple = "taken", ShortTranslate = "брать, хватать, взять" },
                new IrregularVerb { Infinitive = "teach", PastSimple = "taught", PastParticiple = "taught", ShortTranslate = "учить, обучать" },
                new IrregularVerb { Infinitive = "tear", PastSimple = "tore", PastParticiple = "torn", ShortTranslate = "рвать, отрывать" },    
                new IrregularVerb { Infinitive = "tell", PastSimple = "told", PastParticiple = "told", ShortTranslate = "рассказывать" },
                new IrregularVerb { Infinitive = "think", PastSimple = "thought", PastParticiple = "thought", ShortTranslate = "думать, мыслить, размышлять" },
                new IrregularVerb { Infinitive = "throw", PastSimple = "threw", PastParticiple = "thrown", ShortTranslate = "бросать, кидать, метать" },
                new IrregularVerb { Infinitive = "understand", PastSimple = "understood", PastParticiple = "understood", ShortTranslate = "понимать, постигать" },
                new IrregularVerb { Infinitive = "wake", PastSimple = "woke", PastParticiple = "woken", ShortTranslate = "просыпаться, будить" },
                new IrregularVerb { Infinitive = "wear", PastSimple = "wore", PastParticiple = "worn", ShortTranslate = "носить (одежду)" },
                new IrregularVerb { Infinitive = "win", PastSimple = "won", PastParticiple = "won", ShortTranslate = "победить, выиграть" },
                new IrregularVerb { Infinitive = "write", PastSimple = "wrote", PastParticiple = "written", ShortTranslate = "писать, записывать" },
            };   
        }

    }
}
