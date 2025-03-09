using UnityEngine;

public class SwitchObjects : MonoBehaviour
{
    public GameObject object1; // Первый объект
    public GameObject object2; // Второй объект
    public ParticleSystem particleSystem; // Система частиц

    void Start()
    {
        // Изначально активируем только первый объект и систему частиц
        object1.SetActive(true);
        object2.SetActive(false);
        particleSystem.Stop(); // Останавливаем систему частиц
    }

    void Update()
    {
        // Проверяем, произошло ли нажатие правой кнопки мыши
        if (Input.GetMouseButtonDown(1)) // 1 - это правая кнопка мыши
        {
            ToggleObjects();
        }
    }

    void ToggleObjects()
    {
        object1.SetActive(false); // Делаем первый объект неактивным
        object2.SetActive(true); // Делаем второй объект активным
        particleSystem.Play(); // Запускаем систему частиц
    }
}
