using System;
using OneCasa.DataAccess;

namespace OneCasa.BusinessAccess
{
     public class BaseBusinessAccess
    {
        #region Declaration
        private bool _isTransactionRequired;
        public delegate void TransactionMethod();
        protected TransactionMethod operation;
        public BaseDataAccess m_Access;
        #endregion

        #region Public Methods
        public BaseDataAccess Transaction
        {
            get { return m_Access; }
        }

        public TransactionMethod Operation
        {
            set { operation = value; }
        }

        public BaseBusinessAccess()
        {
            m_Access = new BaseDataAccess();
        }

        public BaseBusinessAccess(BaseDataAccess transaction)
        {
            m_Access = transaction;
        }


        public virtual void ExecuteOperation(bool isTransactionRequired, int read)
        {
            try
            {
                _isTransactionRequired = isTransactionRequired;
                if (isTransactionRequired)
                {
                    this.BeginTransaction(read);
                    this.operation();
                    this.Commit();
                }
                else
                {
                    this.OpenConnection(read);
                    this.operation();
                }
            }
            catch (Exception ex)
            {
                RollBack();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public virtual void ExecuteOperation(bool isTransactionRequired)
        {
            try
            {
                _isTransactionRequired = isTransactionRequired;
                if (isTransactionRequired)
                {
                    this.BeginTransaction();
                    this.operation();
                    this.Commit();
                }
                else
                {
                    this.OpenConnection();
                    this.operation();
                }
            }
            catch (Exception ex)
            {
                RollBack();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public virtual void ExecuteOperation(bool isTransactionRequired, string conString)
        {
            try
            {
                _isTransactionRequired = isTransactionRequired;
                if (isTransactionRequired)
                {
                    this.BeginTransaction();
                    this.operation();
                    this.Commit();
                }
                else
                {
                    this.OpenConnection(conString);
                    this.operation();
                }
            }
            catch (Exception ex)
            {
                RollBack();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool Start(bool isTransactionRequired, int read)
        {
            bool success = false;
            try
            {
                this.ExecuteOperation(isTransactionRequired, read);
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (success);
        }
        public bool Start(bool isTransactionRequired)
        {
            bool success = false;
            try
            {
                this.ExecuteOperation(isTransactionRequired);
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (success);
        }

        public bool Start(bool isTransactionRequired, string conString)
        {
            bool success = false;
            try
            {
                this.ExecuteOperation(isTransactionRequired, conString);
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (success);
        }
        #endregion

        #region Private Methods
        private void OpenConnection(int read)
        {
            if (this.m_Access != null)
                this.m_Access.OpenConnection(read);
        }
        private void OpenConnection()
        {
            if (this.m_Access != null)
                this.m_Access.OpenConnection();
        }
        private void OpenConnection(string conString)
        {
            if (this.m_Access != null)
                this.m_Access.OpenConnection(conString);
        }

        private void CloseConnection()
        {
            if (this.m_Access != null)
                this.m_Access.CloseConnection();
        }

        private void BeginTransaction(int read)
        {
            if (this.m_Access != null)
                this.m_Access.BeginTransaction(read);
        }

        private void BeginTransaction()
        {
            if (this.m_Access != null)
                this.m_Access.BeginTransaction();
        }

        private void Commit()
        {
            if (this.m_Access != null)
                this.m_Access.CommitTransaction();
        }

        private void RollBack()
        {
            if (!_isTransactionRequired)
                return;

            if (this.m_Access != null)
                this.m_Access.RollbackTransaction();
        }
        #endregion
    }
}