using System;
using System.Linq;
using System.Collections.Generic;

// Определение интерфейса для сотрудника
public interface IEmployee
{
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime HireDate { get; set; }
    string Position { get; set; }
    decimal Salary { get; set; }
    string Gender { get; set; }
}

// Реализация интерфейса в структуре Employee
public struct Employee : IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public string Gender { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, {Position}, Salary: {Salary:C}, Hire Date: {HireDate.ToShortDateString()}, Gender: {Gender}";
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите количество сотрудников: ");
        int employeeCount = int.Parse(Console.ReadLine());

        // Создание массива сотрудников
        Employee[] employees = new Employee[employeeCount];

        // Заполнение массива
        for (int i = 0; i < employeeCount; i++)
        {
            Console.WriteLine($"Введите информацию о сотруднике #{i + 1}:");

            Console.Write("Имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();

            Console.Write("Дата приема на работу (гггг-мм-дд): ");
            DateTime hireDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Должность: ");
            string position = Console.ReadLine();

            Console.Write("Зарплата: ");
            decimal salary = decimal.Parse(Console.ReadLine());

            Console.Write("Пол (М/Ж): ");
            string gender = Console.ReadLine();

            employees[i] = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                HireDate = hireDate,
                Position = position,
                Salary = salary,
                Gender = gender
            };
        }

        // a. Вывести полную информацию обо всех сотрудниках
        Console.WriteLine("\nПолная информация обо всех сотрудниках:");
        PrintAllEmployeesInfo(employees);

        // b. Вывести полную информацию о сотрудниках выбранной должности
        Console.Write("\nВведите должность для вывода информации: ");
        string selectedPosition = Console.ReadLine();
        PrintEmployeesInfoByPosition(employees, selectedPosition);

        // c. Найти менеджеров с зарплатой выше средней зарплаты клерков
        decimal clerkAverageSalary = employees.Where(e => e.Position == "Clerk").Average(e => e.Salary);
        var managersAboveAverageSalary = employees
            .Where(e => e.Position == "Manager" && e.Salary > clerkAverageSalary)
            .OrderBy(e => e.LastName);
        Console.WriteLine("\nМенеджеры с зарплатой выше средней зарплаты клерков:");
        PrintAllEmployeesInfo(managersAboveAverageSalary);

        // d. Вывести информацию о сотрудниках, принятых на работу позже определенной даты
        Console.Write("\nВведите дату (гггг-мм-дд) для вывода информации о сотрудниках, принятых на работу позже: ");
        DateTime specifiedDate = DateTime.Parse(Console.ReadLine());
        var employeesAfterDate = employees
            .Where(e => e.HireDate > specifiedDate)
            .OrderBy(e => e.LastName);
        Console.WriteLine("\nИнформация о сотрудниках, принятых на работу позже указанной даты:");
        PrintAllEmployeesInfo(employeesAfterDate);

        // e. Вывести информацию о сотрудниках в зависимости от пола
        Console.Write("\nВыберите пол для вывода информации (М/Ж, или любая клавиша для всех): ");
        string selectedGender = Console.ReadLine().ToUpper();
        PrintEmployeesInfoByGender(employees, selectedGender);

        Console.ReadLine();
    }

    // Метод для вывода полной информации обо всех сотрудниках
    static void PrintAllEmployeesInfo(IEnumerable<Employee> employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    // Метод для вывода полной информации о сотрудниках выбранной должности
    static void PrintEmployeesInfoByPosition(IEnumerable<Employee> employees, string position)
    {
        var selectedEmployees = employees.Where(e => e.Position == position);
        Console.WriteLine($"\nИнформация о сотрудниках с должностью '{position}':");
        PrintAllEmployeesInfo(selectedEmployees);
    }

    // Метод для вывода информации о сотрудниках в зависимости от пола
    static void PrintEmployeesInfoByGender(IEnumerable<Employee> employees, string gender)
    {
        if (gender == "М" || gender == "Ж")
        {
            var selectedEmployees = employees.Where(e => e.Gender == gender);
            Console.WriteLine($"\nИнформация о сотрудниках пола '{gender}':");
            PrintAllEmployeesInfo(selectedEmployees);
        }
        else
        {
            Console.WriteLine("\nИнформация о всех сотрудниках:");
            PrintAllEmployeesInfo(employees);
        }
    }
}
