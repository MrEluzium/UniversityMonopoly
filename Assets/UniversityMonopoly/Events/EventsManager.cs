using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Material gainMaterial;
    public Material debuffMaterial;
    public Material guileMaterial;
    public Material restMaterial;
    public Material classMaterial;
    public Material examMaterial;

    public List<EventData> gainEvents = new List<EventData>();
    public List<EventData> debuffEvents = new List<EventData>();
    public List<EventData> guileEvents = new List<EventData>();

    void Start()
    {
        // Gain #1
        EventAbilityData GainOneOne = new EventAbilityData();
        GainOneOne.text = "Использовать момент (можешь кинуть кубик второй раз, -мана)";
        GainOneOne.diceRerollAmount = 1;
        GainOneOne.manaV = -1;

        EventAbilityData GainOneTwo = new EventAbilityData();
        GainOneTwo.text = "Забить";

        gainEvents.Add(new EventData(
            "Наступил вечер, у тебя появился прилив мотивации", 
            GainOneOne, 
            GainOneTwo)
        );

        // Gain #2
        EventAbilityData GainTowOne = new EventAbilityData();
        GainTowOne.text = "Пойти (+2 к ходу на 3 хода, -мана)";
        GainTowOne.diceRollBuff = 2;
        GainTowOne.diceRollLenth = 3;
        GainTowOne.manaV = -1;

        EventAbilityData GainTowTwo = new EventAbilityData();
        GainTowTwo.text = "Забить";

        gainEvents.Add(new EventData(
            "Вы получили возможность пойти на марафон", 
            GainTowOne, 
            GainTowTwo)
        );

        // Gain #3
        // EventAbilityData GainThreeOne = new EventAbilityData();
        // GainThreeOne.text = "Поднять руку друга (+мана, и случайный ивент коварства)";
        // GainThreeOne.manaV = 1;

        // EventAbilityData GainThreeTwo = new EventAbilityData();

        // gainEvents.Add(new EventData(
        //     "На лекции задали тяжелый вопрос на который необходимо ответить", 
        //     GainThreeOne, 
        //     GainThreeTwo)
        // );

        // Gain #4
        // EventAbilityData GainFourOne = new EventAbilityData();
        // GainFourOne.text = "Выбрать значение на кубике для следующего хода (в пределах текущих значений твоего кубика, -мана)";
        // GainFourOne.manaV = -1;

        // EventAbilityData GainFourTwo = new EventAbilityData();
        // GainFourTwo.text = "Посижу в корпусе";

        // gainEvents.Add(new EventData(
        //     "У тебя отменили пару, можешь пойти куда хочешь", 
        //     GainFourOne, 
        //     GainFourTwo)
        // );

        // Gain #5
        EventAbilityData GainFiveOne = new EventAbilityData();
        GainFiveOne.text = "Пойти к этому человеку (требования к экзамену уменьшаются на 50%, -мана)";
        GainFiveOne.examRequirementsMultiplier = .5f;
        GainFiveOne.manaV = -1;

        EventAbilityData GainFiveTwo = new EventAbilityData();
        GainFiveTwo.text = "Забить";

        gainEvents.Add(new EventData(
            "На перемене в столовой ты услышал, что кто-то может подготовить к экзамену", 
            GainFiveOne, 
            GainFiveTwo)
        );

        // Debuff #1
        EventAbilityData debuffOneOne = new EventAbilityData();
        debuffOneOne.text = "Пойти на пары (-2 к шагу на два хода)";
        debuffOneOne.diceRollBuff = -2;
        debuffOneOne.diceRollLenth = 2;

        EventAbilityData debuffOneTwo = new EventAbilityData();
        debuffOneTwo.text = "Забить (-50% к получению знаний на следующей паре)";
        debuffOneTwo.knowledgeMultiplaier = .5f;

        debuffEvents.Add(new EventData(
            "Вы вечером решили сыграть в игру перед экзаменом, потеряв счёт времени вы уснули в 5 часов ночи, вы спали 2 часа", 
            debuffOneOne, 
            debuffOneTwo)
        );

        // Debuff #2
        // EventAbilityData debuffTwoOne = new EventAbilityData();
        // debuffTwoOne.text = "Бежать до ПГНИУ (-мана)";
        // debuffTwoOne.manaV = -1;

        // EventAbilityData debuffTwoTwo = new EventAbilityData();
        // debuffTwoTwo.text = "Забить (вы возвращаетесь на прошлую позицию)";
        // debuffTwoTwo.knowledgeMultiplaier = .5f;

        // debuffEvents.Add(new EventData(
        //     "Вы перепутали автобус и поехали в ПНИПУ", 
        //     debuffTwoOne, 
        //     debuffTwoTwo)
        // );

        // Debuff #3
        EventAbilityData debuffThreeOne = new EventAbilityData();
        debuffThreeOne.text = "Это не может быть правдой (-мана)";
        debuffThreeOne.manaV = -1;

        EventAbilityData debuffThreeTwo = new EventAbilityData();
        debuffThreeTwo.text = "В этот раз я отступлю (-мана)";
        debuffThreeTwo.manaV = -1;

        debuffEvents.Add(new EventData(
            "Вы решили пойти на камни, но там никого нет, потому что на камни не ходят каждый день :(", 
            debuffThreeOne, 
            debuffThreeTwo)
        );

        // Debuff #4
        EventAbilityData debuffFourOne = new EventAbilityData();
        debuffFourOne.text = "Ладно, сделаю (пропуск следующего хода)";
        debuffFourOne.turnsToPass = 1;

        EventAbilityData debuffFourTwo = new EventAbilityData();
        debuffFourTwo.text = "Потом найду время (-мана)";
        debuffFourTwo.manaV = -1;

        debuffEvents.Add(new EventData(
            "Вы забыли сделать флюорографию, вам придется выделить день чтобы её сделать", 
            debuffFourOne, 
            debuffFourTwo)
        );

        // Debuff #5
        // EventAbilityData debuffFiveOne = new EventAbilityData();
        // debuffFiveOne.text = "Куда? (телепорт в случайную точку на карте, -мана)";
        // debuffFiveOne.manaV = -1;
        // debuffFiveOne.turnsToPass = 1;

        // EventAbilityData debuffFiveTwo = new EventAbilityData();
        // debuffFiveTwo.text = "Потом найду время (-мана)";
        // debuffFiveTwo.manaV = -1;

        // debuffEvents.Add(new EventData(
        //     "Решив пройти из 5 корпуса во 2 не выходя на улицу, вы потерялись и не понимаете в каком вы корпусе", 
        //     debuffFiveOne, 
        //     debuffFiveTwo)
        // );

        // Guile #1
        // EventAbilityData guileOneOne = new EventAbilityData();
        // guileOneOne.text = "Пусть идёт какой-то игрок (не может сделать один ход -мана)";
        // guileOneOne.manaV = -1;

        // EventAbilityData guileOneTwo = new EventAbilityData();
        // guileOneTwo.text = "Самому идти (пропустить следующего хода)";
        // guileOneTwo.turnsToPass = 1;

        // gainEvents.Add(new EventData(
        //     "Группу пригласили на конференцию с директором, кто-то обязательно должен пойти", 
        //     guileOneOne, 
        //     guileOneTwo)
        // );

        // Guile #2
        // EventAbilityData guileTwoOne = new EventAbilityData();
        // guileTwoOne.text = "Ты просишь помощи но делаешь это без уважения (кидается кубик что отодвигает его назад, -мана)";
        // guileTwoOne.manaV = -1;

        // EventAbilityData guileTwoTwo = new EventAbilityData();
        // guileTwoTwo.text = "Так уж и быть";

        // gainEvents.Add(new EventData(
        //     "Ты просил скинуть задания для лабы у друга, но он не скинул. Теперь он просит у тебя в помощи", 
        //     guileTwoOne, 
        //     guileTwoTwo)
        // );

        // Guile #3
        // EventAbilityData guileThreeOne = new EventAbilityData();
        // guileThreeOne.text = "Отправиться во 2 корпус (телепорт в случайную точку на карте)";

        // EventAbilityData guileThreeTwo = new EventAbilityData();
        // guileThreeTwo.text = "Всем сообщить о том что пара во 2 корпусе (все игроки появляются в случайной точке на карте)";

        // gainEvents.Add(new EventData(
        //     "Тебе сообщили о том что пара будет проходить в другой аудитории", 
        //     guileThreeOne, 
        //     guileThreeTwo)
        // );

        // Guile #4
        // EventAbilityData guileFourOne = new EventAbilityData();
        // guileFourOne.text = "Сделать другого игрока старостой (игрок будет кидать не кубик d6 а кубик d4 на 3 хода)";
        // guileFourOne.manaV = -1;

        // EventAbilityData guileFourTwo = new EventAbilityData();
        // guileFourTwo.text = "Зачем?";

        // gainEvents.Add(new EventData(
        //     "Ваша группа решила переизбрать старосту", 
        //     guileFourOne, 
        //     guileFourTwo)
        // );

        // Guile #5
        // EventAbilityData guileFiveOne = new EventAbilityData();
        // guileFiveOne.text = "Рассказать всем (телепартация всех в случайное место на карте, -мана)";
        // guileFiveOne.manaV = -1;

        // EventAbilityData guileFiveTwo = new EventAbilityData();
        // guileFiveTwo.text = "Держать в секрете новое открытие";

        // gainEvents.Add(new EventData(
        //     "Вы узнали о самом не смешном анекдоте, вы можете его рассказать его своим одногруппникам", 
        //     guileFiveOne, 
        //     guileFiveTwo)
        // );
    }
}
