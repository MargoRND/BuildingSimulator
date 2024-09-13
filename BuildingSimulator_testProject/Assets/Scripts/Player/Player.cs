using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Данные об игроке
    /// </summary>
    public class Player : MonoBehaviour
    {
        [field: Header("Передвижение")]
        /// <summary>
        /// Скорость ходьбы
        /// </summary>
        [field: SerializeField] public float WalkSpeed { get; private set; }

        /// <summary>
        /// Скорость поворота
        /// </summary>
        [field: SerializeField] public float RotationSpeed { get; private set; }

        /// <summary>
        /// Сила прыжка
        /// </summary>
        [field: SerializeField] public float JumpForce { get; private set; }

        /// <summary>
        /// Гравитация для расчета скорости падения
        /// </summary>
        [field: SerializeField] public float Gravity { get; private set; }
    }
}