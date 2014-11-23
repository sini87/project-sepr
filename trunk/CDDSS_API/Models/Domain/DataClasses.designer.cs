﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CDDSS_API.Models.Domain
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="cddss")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertAccessObject(AccessObject instance);
    partial void UpdateAccessObject(AccessObject instance);
    partial void DeleteAccessObject(AccessObject instance);
    partial void InsertAccessRight(AccessRight instance);
    partial void UpdateAccessRight(AccessRight instance);
    partial void DeleteAccessRight(AccessRight instance);
    partial void InsertIssue(Issue instance);
    partial void UpdateIssue(Issue instance);
    partial void DeleteIssue(Issue instance);
    partial void InsertDocument(Document instance);
    partial void UpdateDocument(Document instance);
    partial void DeleteDocument(Document instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, mappingSource)
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
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<AccessObject> AccessObjects
		{
			get
			{
				return this.GetTable<AccessObject>();
			}
		}
		
		public System.Data.Linq.Table<AccessRight> AccessRights
		{
			get
			{
				return this.GetTable<AccessRight>();
			}
		}
		
		public System.Data.Linq.Table<Issue> Issues
		{
			get
			{
				return this.GetTable<Issue>();
			}
		}
		
		public System.Data.Linq.Table<Document> Documents
		{
			get
			{
				return this.GetTable<Document>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Id;
		
		private string _Email;
		
		private bool _EmailConfirmed;
		
		private string _PasswordHash;
		
		private string _SecurityStamp;
		
		private string _PhoneNumber;
		
		private bool _PhoneNumberConfirmed;
		
		private bool _TwoFactorEnabled;
		
		private System.Nullable<System.DateTime> _LockoutEndDateUtc;
		
		private bool _LockoutEnabled;
		
		private int _AccessFailedCount;
		
		private string _UserName;
		
		private System.Nullable<int> _AccessObject;
		
		private string _FirstName;
		
		private string _LastName;
		
		private string _SecretQuestion;
		
		private string _Answer;
		
		private EntityRef<AccessObject> _AccessObject1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(string value);
    partial void OnIdChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnEmailConfirmedChanging(bool value);
    partial void OnEmailConfirmedChanged();
    partial void OnPasswordHashChanging(string value);
    partial void OnPasswordHashChanged();
    partial void OnSecurityStampChanging(string value);
    partial void OnSecurityStampChanged();
    partial void OnPhoneNumberChanging(string value);
    partial void OnPhoneNumberChanged();
    partial void OnPhoneNumberConfirmedChanging(bool value);
    partial void OnPhoneNumberConfirmedChanged();
    partial void OnTwoFactorEnabledChanging(bool value);
    partial void OnTwoFactorEnabledChanged();
    partial void OnLockoutEndDateUtcChanging(System.Nullable<System.DateTime> value);
    partial void OnLockoutEndDateUtcChanged();
    partial void OnLockoutEnabledChanging(bool value);
    partial void OnLockoutEnabledChanged();
    partial void OnAccessFailedCountChanging(int value);
    partial void OnAccessFailedCountChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnAccessObjectChanging(System.Nullable<int> value);
    partial void OnAccessObjectChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnSecretQuestionChanging(string value);
    partial void OnSecretQuestionChanged();
    partial void OnAnswerChanging(string value);
    partial void OnAnswerChanged();
    #endregion
		
		public User()
		{
			this._AccessObject1 = default(EntityRef<AccessObject>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(256)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailConfirmed", DbType="Bit NOT NULL")]
		public bool EmailConfirmed
		{
			get
			{
				return this._EmailConfirmed;
			}
			set
			{
				if ((this._EmailConfirmed != value))
				{
					this.OnEmailConfirmedChanging(value);
					this.SendPropertyChanging();
					this._EmailConfirmed = value;
					this.SendPropertyChanged("EmailConfirmed");
					this.OnEmailConfirmedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordHash", DbType="NVarChar(MAX)")]
		public string PasswordHash
		{
			get
			{
				return this._PasswordHash;
			}
			set
			{
				if ((this._PasswordHash != value))
				{
					this.OnPasswordHashChanging(value);
					this.SendPropertyChanging();
					this._PasswordHash = value;
					this.SendPropertyChanged("PasswordHash");
					this.OnPasswordHashChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecurityStamp", DbType="NVarChar(MAX)")]
		public string SecurityStamp
		{
			get
			{
				return this._SecurityStamp;
			}
			set
			{
				if ((this._SecurityStamp != value))
				{
					this.OnSecurityStampChanging(value);
					this.SendPropertyChanging();
					this._SecurityStamp = value;
					this.SendPropertyChanged("SecurityStamp");
					this.OnSecurityStampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhoneNumber", DbType="NVarChar(MAX)")]
		public string PhoneNumber
		{
			get
			{
				return this._PhoneNumber;
			}
			set
			{
				if ((this._PhoneNumber != value))
				{
					this.OnPhoneNumberChanging(value);
					this.SendPropertyChanging();
					this._PhoneNumber = value;
					this.SendPropertyChanged("PhoneNumber");
					this.OnPhoneNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhoneNumberConfirmed", DbType="Bit NOT NULL")]
		public bool PhoneNumberConfirmed
		{
			get
			{
				return this._PhoneNumberConfirmed;
			}
			set
			{
				if ((this._PhoneNumberConfirmed != value))
				{
					this.OnPhoneNumberConfirmedChanging(value);
					this.SendPropertyChanging();
					this._PhoneNumberConfirmed = value;
					this.SendPropertyChanged("PhoneNumberConfirmed");
					this.OnPhoneNumberConfirmedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TwoFactorEnabled", DbType="Bit NOT NULL")]
		public bool TwoFactorEnabled
		{
			get
			{
				return this._TwoFactorEnabled;
			}
			set
			{
				if ((this._TwoFactorEnabled != value))
				{
					this.OnTwoFactorEnabledChanging(value);
					this.SendPropertyChanging();
					this._TwoFactorEnabled = value;
					this.SendPropertyChanged("TwoFactorEnabled");
					this.OnTwoFactorEnabledChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LockoutEndDateUtc", DbType="DateTime")]
		public System.Nullable<System.DateTime> LockoutEndDateUtc
		{
			get
			{
				return this._LockoutEndDateUtc;
			}
			set
			{
				if ((this._LockoutEndDateUtc != value))
				{
					this.OnLockoutEndDateUtcChanging(value);
					this.SendPropertyChanging();
					this._LockoutEndDateUtc = value;
					this.SendPropertyChanged("LockoutEndDateUtc");
					this.OnLockoutEndDateUtcChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LockoutEnabled", DbType="Bit NOT NULL")]
		public bool LockoutEnabled
		{
			get
			{
				return this._LockoutEnabled;
			}
			set
			{
				if ((this._LockoutEnabled != value))
				{
					this.OnLockoutEnabledChanging(value);
					this.SendPropertyChanging();
					this._LockoutEnabled = value;
					this.SendPropertyChanged("LockoutEnabled");
					this.OnLockoutEnabledChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccessFailedCount", DbType="Int NOT NULL")]
		public int AccessFailedCount
		{
			get
			{
				return this._AccessFailedCount;
			}
			set
			{
				if ((this._AccessFailedCount != value))
				{
					this.OnAccessFailedCountChanging(value);
					this.SendPropertyChanging();
					this._AccessFailedCount = value;
					this.SendPropertyChanged("AccessFailedCount");
					this.OnAccessFailedCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccessObject", DbType="Int")]
		public System.Nullable<int> AccessObject
		{
			get
			{
				return this._AccessObject;
			}
			set
			{
				if ((this._AccessObject != value))
				{
					if (this._AccessObject1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAccessObjectChanging(value);
					this.SendPropertyChanging();
					this._AccessObject = value;
					this.SendPropertyChanged("AccessObject");
					this.OnAccessObjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(40)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(40)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecretQuestion", DbType="VarChar(256)")]
		public string SecretQuestion
		{
			get
			{
				return this._SecretQuestion;
			}
			set
			{
				if ((this._SecretQuestion != value))
				{
					this.OnSecretQuestionChanging(value);
					this.SendPropertyChanging();
					this._SecretQuestion = value;
					this.SendPropertyChanged("SecretQuestion");
					this.OnSecretQuestionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Answer", DbType="VarChar(40)")]
		public string Answer
		{
			get
			{
				return this._Answer;
			}
			set
			{
				if ((this._Answer != value))
				{
					this.OnAnswerChanging(value);
					this.SendPropertyChanging();
					this._Answer = value;
					this.SendPropertyChanged("Answer");
					this.OnAnswerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="AccessObject_User", Storage="_AccessObject1", ThisKey="AccessObject", OtherKey="Id", IsForeignKey=true)]
		public AccessObject AccessObject1
		{
			get
			{
				return this._AccessObject1.Entity;
			}
			set
			{
				AccessObject previousValue = this._AccessObject1.Entity;
				if (((previousValue != value) 
							|| (this._AccessObject1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._AccessObject1.Entity = null;
						previousValue.Users.Remove(this);
					}
					this._AccessObject1.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._AccessObject = value.Id;
					}
					else
					{
						this._AccessObject = default(Nullable<int>);
					}
					this.SendPropertyChanged("AccessObject1");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AccessObject")]
	public partial class AccessObject : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private EntitySet<User> _Users;
		
		private EntitySet<AccessRight> _AccessRights;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    #endregion
		
		public AccessObject()
		{
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			this._AccessRights = new EntitySet<AccessRight>(new Action<AccessRight>(this.attach_AccessRights), new Action<AccessRight>(this.detach_AccessRights));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="AccessObject_User", Storage="_Users", ThisKey="Id", OtherKey="AccessObject")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="AccessObject_AccessRight", Storage="_AccessRights", ThisKey="Id", OtherKey="AccessObject")]
		public EntitySet<AccessRight> AccessRights
		{
			get
			{
				return this._AccessRights;
			}
			set
			{
				this._AccessRights.Assign(value);
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
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.AccessObject1 = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.AccessObject1 = null;
		}
		
		private void attach_AccessRights(AccessRight entity)
		{
			this.SendPropertyChanging();
			entity.AccessObject1 = this;
		}
		
		private void detach_AccessRights(AccessRight entity)
		{
			this.SendPropertyChanging();
			entity.AccessObject1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AccessRight")]
	public partial class AccessRight : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _AccessObject;
		
		private int _Issue;
		
		private char _Right;
		
		private EntityRef<AccessObject> _AccessObject1;
		
		private EntityRef<Issue> _Issue1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAccessObjectChanging(int value);
    partial void OnAccessObjectChanged();
    partial void OnIssueChanging(int value);
    partial void OnIssueChanged();
    partial void OnRightChanging(char value);
    partial void OnRightChanged();
    #endregion
		
		public AccessRight()
		{
			this._AccessObject1 = default(EntityRef<AccessObject>);
			this._Issue1 = default(EntityRef<Issue>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccessObject", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int AccessObject
		{
			get
			{
				return this._AccessObject;
			}
			set
			{
				if ((this._AccessObject != value))
				{
					if (this._AccessObject1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAccessObjectChanging(value);
					this.SendPropertyChanging();
					this._AccessObject = value;
					this.SendPropertyChanged("AccessObject");
					this.OnAccessObjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Issue", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Issue
		{
			get
			{
				return this._Issue;
			}
			set
			{
				if ((this._Issue != value))
				{
					if (this._Issue1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIssueChanging(value);
					this.SendPropertyChanging();
					this._Issue = value;
					this.SendPropertyChanged("Issue");
					this.OnIssueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Right]", Storage="_Right", DbType="Char(1) NOT NULL")]
		public char Right
		{
			get
			{
				return this._Right;
			}
			set
			{
				if ((this._Right != value))
				{
					this.OnRightChanging(value);
					this.SendPropertyChanging();
					this._Right = value;
					this.SendPropertyChanged("Right");
					this.OnRightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="AccessObject_AccessRight", Storage="_AccessObject1", ThisKey="AccessObject", OtherKey="Id", IsForeignKey=true)]
		public AccessObject AccessObject1
		{
			get
			{
				return this._AccessObject1.Entity;
			}
			set
			{
				AccessObject previousValue = this._AccessObject1.Entity;
				if (((previousValue != value) 
							|| (this._AccessObject1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._AccessObject1.Entity = null;
						previousValue.AccessRights.Remove(this);
					}
					this._AccessObject1.Entity = value;
					if ((value != null))
					{
						value.AccessRights.Add(this);
						this._AccessObject = value.Id;
					}
					else
					{
						this._AccessObject = default(int);
					}
					this.SendPropertyChanged("AccessObject1");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_AccessRight", Storage="_Issue1", ThisKey="Issue", OtherKey="Id", IsForeignKey=true)]
		public Issue Issue1
		{
			get
			{
				return this._Issue1.Entity;
			}
			set
			{
				Issue previousValue = this._Issue1.Entity;
				if (((previousValue != value) 
							|| (this._Issue1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Issue1.Entity = null;
						previousValue.AccessRights.Remove(this);
					}
					this._Issue1.Entity = value;
					if ((value != null))
					{
						value.AccessRights.Add(this);
						this._Issue = value.Id;
					}
					else
					{
						this._Issue = default(int);
					}
					this.SendPropertyChanged("Issue1");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Issue")]
	public partial class Issue : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Title;
		
		private string _Status;
		
		private string _Description;
		
		private System.Nullable<int> _RelatedTo;
		
		private System.Nullable<char> _RelationType;
		
		private System.Nullable<double> _ReviewRating;
		
		private EntitySet<AccessRight> _AccessRights;
		
		private EntitySet<Issue> _Issues;
		
		private EntitySet<Document> _Documents;
		
		private EntityRef<Issue> _Issue1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnRelatedToChanging(System.Nullable<int> value);
    partial void OnRelatedToChanged();
    partial void OnRelationTypeChanging(System.Nullable<char> value);
    partial void OnRelationTypeChanged();
    partial void OnReviewRatingChanging(System.Nullable<double> value);
    partial void OnReviewRatingChanged();
    #endregion
		
		public Issue()
		{
			this._AccessRights = new EntitySet<AccessRight>(new Action<AccessRight>(this.attach_AccessRights), new Action<AccessRight>(this.detach_AccessRights));
			this._Issues = new EntitySet<Issue>(new Action<Issue>(this.attach_Issues), new Action<Issue>(this.detach_Issues));
			this._Documents = new EntitySet<Document>(new Action<Document>(this.attach_Documents), new Action<Document>(this.detach_Documents));
			this._Issue1 = default(EntityRef<Issue>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="VarChar(8000)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RelatedTo", DbType="Int")]
		public System.Nullable<int> RelatedTo
		{
			get
			{
				return this._RelatedTo;
			}
			set
			{
				if ((this._RelatedTo != value))
				{
					if (this._Issue1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRelatedToChanging(value);
					this.SendPropertyChanging();
					this._RelatedTo = value;
					this.SendPropertyChanged("RelatedTo");
					this.OnRelatedToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RelationType", DbType="Char(1)")]
		public System.Nullable<char> RelationType
		{
			get
			{
				return this._RelationType;
			}
			set
			{
				if ((this._RelationType != value))
				{
					this.OnRelationTypeChanging(value);
					this.SendPropertyChanging();
					this._RelationType = value;
					this.SendPropertyChanged("RelationType");
					this.OnRelationTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReviewRating", DbType="Float")]
		public System.Nullable<double> ReviewRating
		{
			get
			{
				return this._ReviewRating;
			}
			set
			{
				if ((this._ReviewRating != value))
				{
					this.OnReviewRatingChanging(value);
					this.SendPropertyChanging();
					this._ReviewRating = value;
					this.SendPropertyChanged("ReviewRating");
					this.OnReviewRatingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_AccessRight", Storage="_AccessRights", ThisKey="Id", OtherKey="Issue")]
		public EntitySet<AccessRight> AccessRights
		{
			get
			{
				return this._AccessRights;
			}
			set
			{
				this._AccessRights.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Issue", Storage="_Issues", ThisKey="Id", OtherKey="RelatedTo")]
		public EntitySet<Issue> Issues
		{
			get
			{
				return this._Issues;
			}
			set
			{
				this._Issues.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Document", Storage="_Documents", ThisKey="Id", OtherKey="Issue")]
		public EntitySet<Document> Documents
		{
			get
			{
				return this._Documents;
			}
			set
			{
				this._Documents.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Issue", Storage="_Issue1", ThisKey="RelatedTo", OtherKey="Id", IsForeignKey=true)]
		public Issue Issue1
		{
			get
			{
				return this._Issue1.Entity;
			}
			set
			{
				Issue previousValue = this._Issue1.Entity;
				if (((previousValue != value) 
							|| (this._Issue1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Issue1.Entity = null;
						previousValue.Issues.Remove(this);
					}
					this._Issue1.Entity = value;
					if ((value != null))
					{
						value.Issues.Add(this);
						this._RelatedTo = value.Id;
					}
					else
					{
						this._RelatedTo = default(Nullable<int>);
					}
					this.SendPropertyChanged("Issue1");
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
		
		private void attach_AccessRights(AccessRight entity)
		{
			this.SendPropertyChanging();
			entity.Issue1 = this;
		}
		
		private void detach_AccessRights(AccessRight entity)
		{
			this.SendPropertyChanging();
			entity.Issue1 = null;
		}
		
		private void attach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.Issue1 = this;
		}
		
		private void detach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.Issue1 = null;
		}
		
		private void attach_Documents(Document entity)
		{
			this.SendPropertyChanging();
			entity.Issue1 = this;
		}
		
		private void detach_Documents(Document entity)
		{
			this.SendPropertyChanging();
			entity.Issue1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Document")]
	public partial class Document : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Issue;
		
		private string _Name;
		
		private System.Data.Linq.Binary _File;
		
		private EntityRef<Issue> _Issue1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIssueChanging(int value);
    partial void OnIssueChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnFileChanging(System.Data.Linq.Binary value);
    partial void OnFileChanged();
    #endregion
		
		public Document()
		{
			this._Issue1 = default(EntityRef<Issue>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Issue", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Issue
		{
			get
			{
				return this._Issue;
			}
			set
			{
				if ((this._Issue != value))
				{
					if (this._Issue1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIssueChanging(value);
					this.SendPropertyChanging();
					this._Issue = value;
					this.SendPropertyChanged("Issue");
					this.OnIssueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(40) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[File]", Storage="_File", DbType="VarBinary(MAX) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary File
		{
			get
			{
				return this._File;
			}
			set
			{
				if ((this._File != value))
				{
					this.OnFileChanging(value);
					this.SendPropertyChanging();
					this._File = value;
					this.SendPropertyChanged("File");
					this.OnFileChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Document", Storage="_Issue1", ThisKey="Issue", OtherKey="Id", IsForeignKey=true)]
		public Issue Issue1
		{
			get
			{
				return this._Issue1.Entity;
			}
			set
			{
				Issue previousValue = this._Issue1.Entity;
				if (((previousValue != value) 
							|| (this._Issue1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Issue1.Entity = null;
						previousValue.Documents.Remove(this);
					}
					this._Issue1.Entity = value;
					if ((value != null))
					{
						value.Documents.Add(this);
						this._Issue = value.Id;
					}
					else
					{
						this._Issue = default(int);
					}
					this.SendPropertyChanged("Issue1");
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
}
#pragma warning restore 1591