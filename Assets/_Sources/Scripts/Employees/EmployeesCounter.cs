using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EmployeesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;
    
    private List<Employee> _employees;
    
    public event Action EmployeesEnd;
    
    private void Awake()
    {
        _employees = FindObjectsOfType<Employee>().ToList();

        foreach (Employee employee in _employees)
            employee.MoneyTaken += OnEmployeeMoneyTaken;
        
        UpdateValue();
    }

    private void OnEmployeeMoneyTaken(Employee employee)
    {
        employee.MoneyTaken -= OnEmployeeMoneyTaken;
        _employees.Remove(employee);

        UpdateValue();

        if (_employees.Count == 0)
            EmployeesEnd?.Invoke();
    }

    private void UpdateValue()
    {
        _counter.text = _employees.Count.ToString();
    }
}