using System.Collections;
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

    private readonly string _getGunPhrase = "Вот теперь можно раздать ЗП!";

    private readonly string[] _startPhrases =
    {
        "Так, какой сегодня день?",
        "Блин! Сегодня же день зарплаты!",
        "Где мой деньгомёт???",
        "...",
        "Нельзя чтобы они меня поймали!",
        "Иначе они отберут у меня ВСЁ!"
    };

    [SerializeField] private Bubble _bubblePrefab;

    private Bubble _currentBubble;
    
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

    [ContextMenu(nameof(SayGunPhrase))]
    public void SayGunPhrase()
    {
        SayPhrase(_getGunPhrase);
    }

    [ContextMenu(nameof(SayStartPhrases))]
    public void SayStartPhrases()
    {
        StartCoroutine(SayStartPhrasesRoutine());
    }

    private IEnumerator SayStartPhrasesRoutine()
    {
        foreach (var phrase in _startPhrases)
        {
            SayPhrase(phrase);
            yield return new WaitForSeconds(3);
        }
    }

    private void SayRandom(IReadOnlyList<string> phrases)
    {
        string phrase = phrases[Random.Range(0, phrases.Count)];
        SayPhrase(phrase);
    }

    private void SayPhrase(string phrase)
    {
        if (_currentBubble != null)
        {
            _currentBubble.Destroyed -= OnBubbleDestroyed;
            Destroy(_currentBubble.gameObject);
            _currentBubble = null;
        }
        
        _currentBubble = Instantiate(_bubblePrefab, transform);
        _currentBubble.Init(phrase);
        _currentBubble.Destroyed += OnBubbleDestroyed;
    }

    private void OnBubbleDestroyed()
    {
        _currentBubble.Destroyed -= OnBubbleDestroyed;
        _currentBubble = null;
    }
}