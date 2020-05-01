using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace OneCasa.DataAccess
{
     public class BaseDataAccess
    {
        #region Declaration
        private SqlConnection _sqlconnection = null;
        private SqlCommand _command = null;
        private SqlTransaction _transaction = null;
        private SqlDataAdapter _adapter = null;
        #endregion

        #region Public Properties
        public List<SqlParameter> DBParameters { get; set; }
        public virtual string ConString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            }
        }
        public virtual string ReadConString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            }
        }
        public virtual int ConnectionTimeout
        {
            get
            {
                int timeout = 0;
                if (ConfigurationManager.AppSettings["ConnectionTimeOut"] != null)
                {
                    int.TryParse(ConfigurationManager.AppSettings["ConnectionTimeOut"], out timeout);
                }
                return timeout;
            }
        }
        public SqlTransaction Transaction { get { return _transaction; } }
        #endregion

        #region Constructor
        public BaseDataAccess()
        {
            DBParameters = new List<SqlParameter>();
        }

        public BaseDataAccess(BaseDataAccess baseAccess)
        {
            DBParameters = new List<SqlParameter>();
            this._sqlconnection = baseAccess._sqlconnection;
            this._transaction = baseAccess._transaction;
            this._adapter = baseAccess._adapter;
        }
        #endregion

        #region Public Methods

        protected DataSet ExecuteDataSet(string spName)
        {
            try
            {
                DataSet recordsDs = new DataSet();
                _command = _sqlconnection.CreateCommand();
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandText = spName;
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddRange(DBParameters.ToArray());

                if (_adapter == null)
                    _adapter = new SqlDataAdapter();
                _adapter.SelectCommand = _command;
                _adapter.Fill(recordsDs);

                return recordsDs;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected IDataReader ExecuteReader(string spName, int read)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection(read);
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandText = spName;
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddRange(DBParameters.ToArray());
                return _command.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected IDataReader ExecuteReader(string spName)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection();
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandText = spName;
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddRange(DBParameters.ToArray());
                return _command.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected object ExecuteScalar(string spName, int read)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection(read);
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandText = spName;
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddRange(DBParameters.ToArray());
                return _command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected object ExecuteScalar(string spName)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection();
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandText = spName;
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddRange(DBParameters.ToArray());
                return _command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected object ExecuteNonQuery(string spName, int read)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection(read);
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandText = spName;
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddRange(DBParameters.ToArray());
                return _command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected object ExecuteNonQuery(string spName)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection();
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandText = spName;
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddRange(DBParameters.ToArray());
                return _command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Parameters

        protected void AddParameter(string name, object value)
        {
            DBParameters.Add(new SqlParameter(name, value));
        }
        protected void AddParameter(string name, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Direction = direction;
            parameter.Size = -1;
            DBParameters.Add(parameter);
        }
        protected void AddParameter(string name, object value, ParameterDirection direction, byte precision, byte scale)
        {
            SqlParameter parameter = new SqlParameter(name, value);
            parameter.Direction = direction;
            parameter.Scale = scale;
            parameter.Precision = precision;
            DBParameters.Add(parameter);
        }

        protected void AddParameter(string name, object value, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter(name, value);
            parameter.Direction = direction;
            DBParameters.Add(parameter);
        }

        protected object GetOutParameterValue(string parameterName)
        {
            if (_command != null)
            {
                return _command.Parameters[parameterName].Value;
            }
            return null;
        }
        #endregion

        #region SaveData
        protected bool SaveData(string spName, int read)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection(read);
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandText = spName;
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddRange(DBParameters.ToArray());

                int result = _command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool SaveData(string spName)
        {
            try
            {
                if (_sqlconnection == null)
                {
                    OpenConnection();
                }
                _command = _sqlconnection.CreateCommand();
                _command.CommandTimeout = ConnectionTimeout;
                _command.CommandText = spName;
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddRange(DBParameters.ToArray());

                int result = _command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Transaction Members

        public bool BeginTransaction(int read)
        {
            try
            {
                bool IsOK = this.OpenConnection(read);

                if (IsOK)
                    _transaction = _sqlconnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }

            return true;
        }

        public bool BeginTransaction()
        {
            try
            {
                bool IsOK = this.OpenConnection();

                if (IsOK)
                    _transaction = _sqlconnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }

            return true;
        }

        public bool CommitTransaction()
        {
            try
            {
                _transaction.Commit();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
            return true;
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
            return;
        }

        public bool OpenConnection()
        {
            try
            {

                if (this._sqlconnection == null)
                    _sqlconnection = new SqlConnection(ConString);

                if (this._sqlconnection.State != ConnectionState.Open)
                {
                    this._sqlconnection.Open();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool OpenConnection(string conString)
        {
            try
            {

                if (this._sqlconnection == null)
                    _sqlconnection = new SqlConnection(conString);

                if (this._sqlconnection.State != ConnectionState.Open)
                {
                    this._sqlconnection.Open();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool OpenConnection(int read)
        {
            try
            {

                if (this._sqlconnection == null)
                    _sqlconnection = new SqlConnection(ReadConString);

                if (this._sqlconnection.State != ConnectionState.Open)
                {
                    this._sqlconnection.Open();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public bool CloseConnection()
        {
            try
            {
                if (this._sqlconnection.State != ConnectionState.Closed)
                    this._sqlconnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlconnection != null)
                {
                    _sqlconnection.Dispose();
                    this._sqlconnection = null;
                }


            }
            return true;
        }

        #endregion

        #region Utility Functions
        protected long GetFieldValue(IDataReader sqlReader, string fieldName, long defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? 0L : sqlReader.GetInt64(pos);
        }

        protected int GetFieldValue(IDataReader sqlReader, string fieldName, int defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? 0 : sqlReader.GetInt32(pos);
        }

        protected Guid GetFieldValue(IDataReader sqlReader, string fieldName, Guid defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? Guid.NewGuid() : (Guid)sqlReader.GetGuid(pos);
        }

        protected float GetFieldValue(IDataReader sqlReader, string fieldName, float defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? 0 : (float)sqlReader.GetDouble(pos);
        }

        protected double GetFieldValue(IDataReader sqlReader, string fieldName, double defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? 0 : sqlReader.GetDouble(pos);
        }
        protected decimal GetFieldValue(IDataReader sqlReader, string fieldName, decimal defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? 0 : sqlReader.GetDecimal(pos);
        }

        protected string GetFieldValue(IDataReader sqlReader, string fieldName, string defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? String.Empty : sqlReader.GetString(pos);
        }

        protected DateTime GetFieldValue(IDataReader sqlReader, string fieldName, DateTime defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? DateTime.MinValue.AddYears(1900) : sqlReader.GetDateTime(pos);
        }
        protected DateTime? GetEndDate(IDataReader sqlReader, string fieldName, DateTime? defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? DateTime.MinValue.AddYears(2100) : sqlReader.GetDateTime(pos);
        }

        protected bool GetFieldValue(IDataReader sqlReader, string fieldName, bool defaultValue)
        {
            int pos = sqlReader.GetOrdinal(fieldName);
            return sqlReader.IsDBNull(pos) ? false : sqlReader.GetBoolean(pos);
        }
        #endregion
    }
}