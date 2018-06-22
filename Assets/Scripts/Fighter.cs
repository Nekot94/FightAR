using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    public int health = 100; // жизни персонажа
    public int damage = 10; // урон по врагу

    public Transform fist; // положение кулака
    public Transform target; // положение цели
    public float damageDistance = 1; // расстояние на котором сработает удар

    public Slider healthSlider; // Слайдер жизней

    Animator anim; // аниматор
    bool isDead; // если умер

	void Start ()
    {
        anim = GetComponent<Animator>(); // Получаем компонент аниматор	
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

	/// <summary>
    /// Проводит атаку
    /// </summary>
    public void Attack()
    {
        if (isDead)
            return;

        anim.SetTrigger("Punch");

        // проверяем дистанцию до врага, если она меньше DamageDistance то наносим урон
        if (Vector3.Distance(fist.position, target.position) <= damageDistance) 
            DealDamage();
    }

    /// <summary>
    /// Наносит урон цели
    /// </summary>
    void DealDamage()
    {
        if (target.GetComponentInChildren<Renderer>().enabled == false)
            return;

        var enemy = target.GetComponent<Fighter>(); // получаем компонент врага
        if (enemy != null) // если компонент есть
        {
            enemy.health -= damage; // отнимаем жизни у врага по колличеству нашего урона
            enemy.healthSlider.value = enemy.health; // Изменить жизни вроага на слайдере
            if (enemy.health <= 0) // если жизни у врага меньше 0, то враг умирает
                enemy.Die();
        }
    }

    /// <summary>
    /// смерть персонажа
    /// </summary>
    public void Die()
    {
        anim.SetBool("IsDead", true); // запускаем анимацию смерти
        isDead = true; // Помечаем мертвым
    }
}
