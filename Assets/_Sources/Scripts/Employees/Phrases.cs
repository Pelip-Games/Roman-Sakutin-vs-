using System.Collections.Generic;
using UnityEngine;

public class Phrases : MonoBehaviour
{    
    private readonly string[] _stalkering =
    {
        "Когда зарплата?",
        "Дайте отпуск",
        "Возьмите в штат",
        "Верните деньги за курс!",
        "Роман Владимирович, нужно поговорить",
        "На подпись!!!",
        "У Хауди Хо курсы лучше",
        "Сакура полезный персонаж",
        "Я великий разработчик, хочу повышение!",
        "Продажи упали",
        "Лёша опять про***лся!"
    };

    private readonly string[] _moneyTaking =
    {
        "Я на Мальдивы!",
        "Вот она безбедная молодость)",
        "Увольнительная на столе",
        "Да-а-а-а-а. ГК реально прибыльная тема"
    };

    private readonly string[] _missing =
    {
        "Наверное, показалось",
        "Не очень-то и хотелось",
        "Поработать, что ли? Да не...",
        "Опять мид ганкают"
    };

    private string _getGunPhrase = "Вот теперь можно раздать ЗП!";

    [SerializeField] private Bubble _bubblePrefab;

    [ContextMenu(nameof(SayStalkerPhrase))]
    public void SayStalkerPhrase()
    {
        SayRandom(_stalkering);
    }

    [ContextMenu(nameof(SayMoneyPhrase))]
    public void SayMoneyPhrase()
    {
        SayRandom(_moneyTaking);
    }

    [ContextMenu(nameof(SayMissPhrase))]
    public void SayMissPhrase()
    {
        SayRandom(_missing);
    }

    [ContextMenu(nameof(SayMissPhrase))]
    public void SayGunPhrase()
    {
        Debug.Log(_getGunPhrase);
        Bubble bubble = Instantiate(_bubblePrefab, transform);
        bubble.Init(_getGunPhrase);
    }

    private void SayRandom(IReadOnlyList<string> phrases)
    {
        string phrase = phrases[Random.Range(0, phrases.Count)];

        Bubble bubble = Instantiate(_bubblePrefab, transform);
        bubble.Init(phrase);
    }
}