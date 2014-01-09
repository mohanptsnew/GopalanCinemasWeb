#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

#endregion

///<summary>
/// Name          : Karthik T
/// Created Date  : June 06,2011
/// Description   : DB Utility
/// Modified Date :
///</summary>

namespace GopalanCinemasDL.DBUtils
{
    public abstract class dbUtils<TParameter, TDataReader, TConnection, TTransaction, TDataAdapter, TCommand> : IDisposable
        where TParameter : DbParameter, IDataParameter
        where TDataReader : DbDataReader, IDataReader
        where TConnection : DbConnection, IDbConnection, new()
        where TTransaction : DbTransaction, IDbTransaction
        where TDataAdapter : DbDataAdapter, IDataAdapter, new()
        where TCommand : DbCommand, IDbCommand, new()
    {
        #region dbConnection
        protected TConnection _conn;
        protected TTransaction _trans;
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="ConnectionString"></param>
        public dbUtils(string ConnectionString)
        {
            _conn = new TConnection();
            _conn.ConnectionString = ConnectionString;
        }
        #endregion

        #region Connection Property
        /// <summary>
        /// To get connection
        /// </summary>
        public TConnection Connection
        {
            get
            {
                return _conn;
            }
        }
        /// <summary>
        /// To do transaction
        /// </summary>
        public TTransaction Transaction
        {
            get
            {
                return _trans;
            }
        }
        #endregion

        #region Execute Reader
        /// <summary>
        /// Execute reader to return datatable
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        protected TDataReader ExecuteReader(string StoreProcName, List<TParameter> Params)
        {
            bool internalOpen = false;
            TCommand cmd;

            try
            {

                cmd = new TCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreProcName;
                if (_trans != default(TTransaction))
                    cmd.Transaction = _trans;
                else
                    cmd.Connection = _conn;

                if (Params != null || Params.Count > 0)
                {
                    foreach (DbParameter param in Params)
                        cmd.Parameters.Add(param);
                }

                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                    internalOpen = true;
                }


                return (TDataReader)cmd.ExecuteReader();

            }
            catch (DbException DbEx)
            {
                throw DbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (internalOpen)
                    _conn.Close();
            }
        }
        /// <summary>
        /// Execute Reader with out params
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <returns></returns>
        protected TDataReader ExecuteReader(string StoreProcName)
        {
            return ExecuteReader(StoreProcName);
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// To return dataset - with params
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        protected DataSet ExecuteDataSet(string StoreProcName, List<TParameter> Params)
        {
            bool internalOpen = false;
            DataSet ds = null;
            TDataAdapter da;
            TCommand cmd;


            try
            {
                ds = new DataSet();
                da = new TDataAdapter();
                cmd = new TCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreProcName;
                if (_trans != default(TTransaction))
                    cmd.Transaction = _trans;
                else
                    cmd.Connection = (TConnection)_conn;

                if (Params != null || Params.Count > 0)
                {
                    foreach (DbParameter param in Params)
                        cmd.Parameters.Add(param);
                }

                da.SelectCommand = cmd;

                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                    internalOpen = true;
                }

                da.Fill(ds);
                return ds;
            }
            catch (DbException DbEx)
            {
                throw DbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (internalOpen)
                    _conn.Close();
            }
        }
        /// <summary>
        /// To return dataset without params
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <returns></returns>
        protected DataSet ExecuteDataSet(string StoreProcName)
        {
            return ExecuteDataSet(StoreProcName, null);
        }
        #endregion

        #region Execute Scalar
        /// <summary>
        /// To execute scalar with params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        protected T ExecuteScalar<T>(string StoreProcName, List<TParameter> Params)
        {
            bool internalOpen = false;
            TCommand cmd;

            try
            {

                cmd = new TCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreProcName;
                if (_trans != default(TTransaction))
                    cmd.Transaction = _trans;
                else
                    cmd.Connection = _conn;

                if (Params != null || Params.Count > 0)
                {
                    foreach (DbParameter param in Params)
                        cmd.Parameters.Add(param);
                }

                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                    internalOpen = true;
                }


                object retVal = cmd.ExecuteScalar();

                if (retVal is T)
                    return (T)retVal;
                else if (retVal == DBNull.Value)
                    return default(T);
                else
                    throw new Exception("Object returned was of the wrong type.");


            }
            catch (DbException DbEx)
            {
                throw DbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (internalOpen)
                    _conn.Close();
            }

        }
        /// <summary>
        /// To execute scalar without params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcName"></param>
        /// <returns></returns>
        protected T ExecuteScalar<T>(string StoreProcName)
        {
            return ExecuteScalar<T>(StoreProcName);
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// ExecuteNonQuery with params
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string StoreProcName, List<TParameter> Params)
        {
            bool internalOpen = false;
            TCommand cmd;


            try
            {

                cmd = new TCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreProcName;
                if (_trans != default(TTransaction))
                    cmd.Transaction = _trans;
                else
                    cmd.Connection = _conn;

                if (Params != null || Params.Count > 0)
                {
                    foreach (DbParameter param in Params)
                        cmd.Parameters.Add(param);
                }

                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                    internalOpen = true;
                }


                return cmd.ExecuteNonQuery();


            }
            catch (DbException DbEx)
            {
                throw DbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (internalOpen)
                    _conn.Close();
            }
        }
        /// <summary>
        /// ExecuteNonQuery with out params
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string StoreProcName)
        {
            return ExecuteNonQuery(StoreProcName, null);
        }
        #endregion

        #region Get Connection

        #endregion

        #region RollBack Transaction
        /// <summary>
        /// To rollback transaction
        /// </summary>
        /// <returns></returns>
        public bool RollBackTransaction()
        {
            if (_conn != null && _conn.State == ConnectionState.Open && _trans != null)
            {
                _trans.Rollback();
                _conn.Close();

                _trans.Dispose();
                _trans = default(TTransaction);
                return true;
            }
            return false;
        }
        #endregion

        #region Begin Transaction
        /// <summary>
        /// To begin transaction
        /// </summary>
        /// <returns></returns>
        public bool BeginTransaction()
        {
            if (_conn != null && _conn.State == ConnectionState.Closed && _trans == null)
            {
                _conn.Open();
                _trans = (TTransaction)_conn.BeginTransaction();
                return true;
            }
            return false;
        }
        #endregion

        #region Commit Transaction
        /// <summary>
        /// To commit transaction
        /// </summary>
        /// <returns></returns>
        public bool CommitTransaction()
        {
            if (_conn != null && _conn.State == ConnectionState.Open && _trans != null)
            {
                _trans.Commit();
                _conn.Close();

                _trans.Dispose();
                _trans = default(TTransaction);
                return true;
            }
            return false;
        }
        #endregion

        #region Rollback/Save Transaction
        /// <summary>
        /// To rollback transaction
        /// </summary>
        /// <param name="SavePointName"></param>
        /// <returns></returns>
        public abstract bool RollBackTransaction(string SavePointName);
        /// <summary>
        /// To save transaction
        /// </summary>
        /// <param name="SavePointName"></param>
        /// <returns></returns>
        public abstract bool SaveTransactionPoint(string SavePointName);
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Idisposable members
        /// </summary>
        public void Dispose()
        {
            if (_conn != null)
            {
                if (_trans != null)
                {
                    _trans.Rollback();
                    _trans.Dispose();
                }

                if (_conn.State != ConnectionState.Closed)
                    _conn.Close();

                _conn.Dispose();
            }
        }

        ~dbUtils()
        {
            Dispose();
        }

        #endregion

    }
        
}
