using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// ������ �� ������
    /// </summary>
    public class Player : MonoBehaviour
    {
        [field: Header("������������")]
        /// <summary>
        /// �������� ������
        /// </summary>
        [field: SerializeField] public float WalkSpeed { get; private set; }

        /// <summary>
        /// �������� ��������
        /// </summary>
        [field: SerializeField] public float RotationSpeed { get; private set; }

        /// <summary>
        /// ���� ������
        /// </summary>
        [field: SerializeField] public float JumpForce { get; private set; }

        /// <summary>
        /// ���������� ��� ������� �������� �������
        /// </summary>
        [field: SerializeField] public float Gravity { get; private set; }
    }
}