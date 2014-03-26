using System;
using System.Data.Entity;
using Domain;

namespace Infrastructure.Contracts
{
    /// <summary>
    /// ��������� �������� Unit Of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// ����������� ��������� ������������
        /// </summary>
        IGenericRepository<ProductCategory> ProductCategoryRepository { get; }

        /// <summary>
        /// ����������� ������� ������ � ����
        /// </summary>
        IGenericRepository<ApplicationLogStatus> ApplicationLogStatusRepository { get; }

        /// <summary>
        /// ����������� ��������� �������� ������
        /// </summary>
        IGenericRepository<ApplicationLog> ApplicationLogRepository { get; }

        /// <summary>
        /// ����������� ������ �� ��������� �����
        /// </summary>
        IGenericRepository<Application> ApplicationRepository { get; }

        /// <summary>
        /// ����������� ����� � ��������
        /// </summary>
        IGenericRepository<Account> AccountRepository { get; }

        /// <summary>
        /// ����������� ����
        /// </summary>
        IGenericRepository<ApplicationRole> ApplicationRoleRepository { get; }

        /// <summary>
        /// ��������� ��� ��������� ��������� ��������� � ���� ������
        /// </summary>
        void Save();

        /// <summary>
        /// �������� ��������
        /// </summary>
        /// <returns>DbContext</returns>
        DbContext GetContext();
    }
}