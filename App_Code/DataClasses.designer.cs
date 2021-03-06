﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Database")]
public partial class DataClassesDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertCanister(Canister instance);
  partial void UpdateCanister(Canister instance);
  partial void DeleteCanister(Canister instance);
  partial void InsertFile(File instance);
  partial void UpdateFile(File instance);
  partial void DeleteFile(File instance);
  #endregion
	
	public DataClassesDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Canister> Canisters
	{
		get
		{
			return this.GetTable<Canister>();
		}
	}
	
	public System.Data.Linq.Table<File> Files
	{
		get
		{
			return this.GetTable<File>();
		}
	}
	
	public System.Data.Linq.Table<Shorten1> Shorten1s
	{
		get
		{
			return this.GetTable<Shorten1>();
		}
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.DeleteFile")]
	public int DeleteFile([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FileID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> fileID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Flag", DbType="Bit")] ref System.Nullable<bool> flag)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fileID, flag);
		flag = ((System.Nullable<bool>)(result.GetParameterValue(1)));
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertFile")]
	public int InsertFile([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FileName", DbType="NVarChar(MAX)")] string fileName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FileLocation", DbType="NVarChar(MAX)")] string fileLocation, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FileType", DbType="NVarChar(MAX)")] string fileType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FileID", DbType="UniqueIdentifier")] ref System.Nullable<System.Guid> fileID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CanisterID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> canisterID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FileSize", DbType="NVarChar(MAX)")] string fileSize)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fileName, fileLocation, fileType, fileID, canisterID, fileSize);
		fileID = ((System.Nullable<System.Guid>)(result.GetParameterValue(3)));
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertCanister")]
	public int InsertCanister([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CanisterID", DbType="UniqueIdentifier")] ref System.Nullable<System.Guid> canisterID)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), canisterID);
		canisterID = ((System.Nullable<System.Guid>)(result.GetParameterValue(0)));
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.ShortenUrl")]
	public int ShortenUrl([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Url", DbType="NVarChar(MAX)")] string url, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Value", DbType="NVarChar(MAX)")] string value)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), url, value);
		return ((int)(result.ReturnValue));
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Canister")]
public partial class Canister : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.Guid _CanisterID;
	
	private EntityRef<File> _File;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCanisterIDChanging(System.Guid value);
    partial void OnCanisterIDChanged();
    #endregion
	
	public Canister()
	{
		this._File = default(EntityRef<File>);
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanisterID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid CanisterID
	{
		get
		{
			return this._CanisterID;
		}
		set
		{
			if ((this._CanisterID != value))
			{
				if (this._File.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnCanisterIDChanging(value);
				this.SendPropertyChanging();
				this._CanisterID = value;
				this.SendPropertyChanged("CanisterID");
				this.OnCanisterIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="File_Canister", Storage="_File", ThisKey="CanisterID", OtherKey="CanisterID", IsForeignKey=true)]
	public File File
	{
		get
		{
			return this._File.Entity;
		}
		set
		{
			File previousValue = this._File.Entity;
			if (((previousValue != value) 
						|| (this._File.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._File.Entity = null;
					previousValue.Canisters.Remove(this);
				}
				this._File.Entity = value;
				if ((value != null))
				{
					value.Canisters.Add(this);
					this._CanisterID = value.CanisterID;
				}
				else
				{
					this._CanisterID = default(System.Guid);
				}
				this.SendPropertyChanged("File");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Files")]
public partial class File : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.Guid _FileID;
	
	private System.Guid _CanisterID;
	
	private string _FileName;
	
	private string _FileLocation;
	
	private string _FileType;
	
	private string _FileSize;
	
	private EntitySet<Canister> _Canisters;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFileIDChanging(System.Guid value);
    partial void OnFileIDChanged();
    partial void OnCanisterIDChanging(System.Guid value);
    partial void OnCanisterIDChanged();
    partial void OnFileNameChanging(string value);
    partial void OnFileNameChanged();
    partial void OnFileLocationChanging(string value);
    partial void OnFileLocationChanged();
    partial void OnFileTypeChanging(string value);
    partial void OnFileTypeChanged();
    partial void OnFileSizeChanging(string value);
    partial void OnFileSizeChanged();
    #endregion
	
	public File()
	{
		this._Canisters = new EntitySet<Canister>(new Action<Canister>(this.attach_Canisters), new Action<Canister>(this.detach_Canisters));
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid FileID
	{
		get
		{
			return this._FileID;
		}
		set
		{
			if ((this._FileID != value))
			{
				this.OnFileIDChanging(value);
				this.SendPropertyChanging();
				this._FileID = value;
				this.SendPropertyChanged("FileID");
				this.OnFileIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanisterID", DbType="UniqueIdentifier NOT NULL")]
	public System.Guid CanisterID
	{
		get
		{
			return this._CanisterID;
		}
		set
		{
			if ((this._CanisterID != value))
			{
				this.OnCanisterIDChanging(value);
				this.SendPropertyChanging();
				this._CanisterID = value;
				this.SendPropertyChanged("CanisterID");
				this.OnCanisterIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName", DbType="NVarChar(MAX)")]
	public string FileName
	{
		get
		{
			return this._FileName;
		}
		set
		{
			if ((this._FileName != value))
			{
				this.OnFileNameChanging(value);
				this.SendPropertyChanging();
				this._FileName = value;
				this.SendPropertyChanged("FileName");
				this.OnFileNameChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileLocation", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
	public string FileLocation
	{
		get
		{
			return this._FileLocation;
		}
		set
		{
			if ((this._FileLocation != value))
			{
				this.OnFileLocationChanging(value);
				this.SendPropertyChanging();
				this._FileLocation = value;
				this.SendPropertyChanged("FileLocation");
				this.OnFileLocationChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileType", DbType="NVarChar(MAX)")]
	public string FileType
	{
		get
		{
			return this._FileType;
		}
		set
		{
			if ((this._FileType != value))
			{
				this.OnFileTypeChanging(value);
				this.SendPropertyChanging();
				this._FileType = value;
				this.SendPropertyChanged("FileType");
				this.OnFileTypeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileSize", DbType="NVarChar(MAX)")]
	public string FileSize
	{
		get
		{
			return this._FileSize;
		}
		set
		{
			if ((this._FileSize != value))
			{
				this.OnFileSizeChanging(value);
				this.SendPropertyChanging();
				this._FileSize = value;
				this.SendPropertyChanged("FileSize");
				this.OnFileSizeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="File_Canister", Storage="_Canisters", ThisKey="CanisterID", OtherKey="CanisterID")]
	public EntitySet<Canister> Canisters
	{
		get
		{
			return this._Canisters;
		}
		set
		{
			this._Canisters.Assign(value);
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
	private void attach_Canisters(Canister entity)
	{
		this.SendPropertyChanging();
		entity.File = this;
	}
	
	private void detach_Canisters(Canister entity)
	{
		this.SendPropertyChanging();
		entity.File = null;
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Shorten")]
public partial class Shorten1
{
	
	private string _Url;
	
	private string _Value;
	
	public Shorten1()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Url", DbType="NVarChar(MAX)")]
	public string Url
	{
		get
		{
			return this._Url;
		}
		set
		{
			if ((this._Url != value))
			{
				this._Url = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Value", DbType="NVarChar(MAX)")]
	public string Value
	{
		get
		{
			return this._Value;
		}
		set
		{
			if ((this._Value != value))
			{
				this._Value = value;
			}
		}
	}
}
#pragma warning restore 1591
