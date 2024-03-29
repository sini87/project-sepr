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

namespace Client.Model
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="aspnet-Client-20141119124736")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertMembership(Membership instance);
    partial void UpdateMembership(Membership instance);
    partial void DeleteMembership(Membership instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["aspnet_Client_20141119124736ConnectionString1"].ConnectionString, mappingSource)
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
		
		public System.Data.Linq.Table<Membership> Memberships
		{
			get
			{
				return this.GetTable<Membership>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _UserId;
		
		private System.Guid _ApplicationId;
		
		private string _UserName;
		
		private bool _IsAnonymous;
		
		private System.DateTime _LastActivityDate;
		
		private EntityRef<Membership> _Membership;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnApplicationIdChanging(System.Guid value);
    partial void OnApplicationIdChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnIsAnonymousChanging(bool value);
    partial void OnIsAnonymousChanged();
    partial void OnLastActivityDateChanging(System.DateTime value);
    partial void OnLastActivityDateChanged();
    #endregion
		
		public User()
		{
			this._Membership = default(EntityRef<Membership>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApplicationId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				if ((this._ApplicationId != value))
				{
					this.OnApplicationIdChanging(value);
					this.SendPropertyChanging();
					this._ApplicationId = value;
					this.SendPropertyChanged("ApplicationId");
					this.OnApplicationIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsAnonymous", DbType="Bit NOT NULL")]
		public bool IsAnonymous
		{
			get
			{
				return this._IsAnonymous;
			}
			set
			{
				if ((this._IsAnonymous != value))
				{
					this.OnIsAnonymousChanging(value);
					this.SendPropertyChanging();
					this._IsAnonymous = value;
					this.SendPropertyChanged("IsAnonymous");
					this.OnIsAnonymousChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastActivityDate", DbType="DateTime NOT NULL")]
		public System.DateTime LastActivityDate
		{
			get
			{
				return this._LastActivityDate;
			}
			set
			{
				if ((this._LastActivityDate != value))
				{
					this.OnLastActivityDateChanging(value);
					this.SendPropertyChanging();
					this._LastActivityDate = value;
					this.SendPropertyChanged("LastActivityDate");
					this.OnLastActivityDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_Membership", Storage="_Membership", ThisKey="UserId", OtherKey="UserId", IsUnique=true, IsForeignKey=false)]
		public Membership Membership
		{
			get
			{
				return this._Membership.Entity;
			}
			set
			{
				Membership previousValue = this._Membership.Entity;
				if (((previousValue != value) 
							|| (this._Membership.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Membership.Entity = null;
						previousValue.User = null;
					}
					this._Membership.Entity = value;
					if ((value != null))
					{
						value.User = this;
					}
					this.SendPropertyChanged("Membership");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Memberships")]
	public partial class Membership : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _UserId;
		
		private System.Guid _ApplicationId;
		
		private string _Password;
		
		private int _PasswordFormat;
		
		private string _PasswordSalt;
		
		private string _Email;
		
		private string _PasswordQuestion;
		
		private string _PasswordAnswer;
		
		private bool _IsApproved;
		
		private bool _IsLockedOut;
		
		private System.DateTime _CreateDate;
		
		private System.DateTime _LastLoginDate;
		
		private System.DateTime _LastPasswordChangedDate;
		
		private System.DateTime _LastLockoutDate;
		
		private int _FailedPasswordAttemptCount;
		
		private System.DateTime _FailedPasswordAttemptWindowStart;
		
		private int _FailedPasswordAnswerAttemptCount;
		
		private System.DateTime _FailedPasswordAnswerAttemptWindowsStart;
		
		private string _Comment;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnApplicationIdChanging(System.Guid value);
    partial void OnApplicationIdChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnPasswordFormatChanging(int value);
    partial void OnPasswordFormatChanged();
    partial void OnPasswordSaltChanging(string value);
    partial void OnPasswordSaltChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnPasswordQuestionChanging(string value);
    partial void OnPasswordQuestionChanged();
    partial void OnPasswordAnswerChanging(string value);
    partial void OnPasswordAnswerChanged();
    partial void OnIsApprovedChanging(bool value);
    partial void OnIsApprovedChanged();
    partial void OnIsLockedOutChanging(bool value);
    partial void OnIsLockedOutChanged();
    partial void OnCreateDateChanging(System.DateTime value);
    partial void OnCreateDateChanged();
    partial void OnLastLoginDateChanging(System.DateTime value);
    partial void OnLastLoginDateChanged();
    partial void OnLastPasswordChangedDateChanging(System.DateTime value);
    partial void OnLastPasswordChangedDateChanged();
    partial void OnLastLockoutDateChanging(System.DateTime value);
    partial void OnLastLockoutDateChanged();
    partial void OnFailedPasswordAttemptCountChanging(int value);
    partial void OnFailedPasswordAttemptCountChanged();
    partial void OnFailedPasswordAttemptWindowStartChanging(System.DateTime value);
    partial void OnFailedPasswordAttemptWindowStartChanged();
    partial void OnFailedPasswordAnswerAttemptCountChanging(int value);
    partial void OnFailedPasswordAnswerAttemptCountChanged();
    partial void OnFailedPasswordAnswerAttemptWindowsStartChanging(System.DateTime value);
    partial void OnFailedPasswordAnswerAttemptWindowsStartChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    #endregion
		
		public Membership()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApplicationId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				if ((this._ApplicationId != value))
				{
					this.OnApplicationIdChanging(value);
					this.SendPropertyChanging();
					this._ApplicationId = value;
					this.SendPropertyChanged("ApplicationId");
					this.OnApplicationIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(128) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordFormat", DbType="Int NOT NULL")]
		public int PasswordFormat
		{
			get
			{
				return this._PasswordFormat;
			}
			set
			{
				if ((this._PasswordFormat != value))
				{
					this.OnPasswordFormatChanging(value);
					this.SendPropertyChanging();
					this._PasswordFormat = value;
					this.SendPropertyChanged("PasswordFormat");
					this.OnPasswordFormatChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordSalt", DbType="NVarChar(128) NOT NULL", CanBeNull=false)]
		public string PasswordSalt
		{
			get
			{
				return this._PasswordSalt;
			}
			set
			{
				if ((this._PasswordSalt != value))
				{
					this.OnPasswordSaltChanging(value);
					this.SendPropertyChanging();
					this._PasswordSalt = value;
					this.SendPropertyChanged("PasswordSalt");
					this.OnPasswordSaltChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordQuestion", DbType="NVarChar(256)")]
		public string PasswordQuestion
		{
			get
			{
				return this._PasswordQuestion;
			}
			set
			{
				if ((this._PasswordQuestion != value))
				{
					this.OnPasswordQuestionChanging(value);
					this.SendPropertyChanging();
					this._PasswordQuestion = value;
					this.SendPropertyChanged("PasswordQuestion");
					this.OnPasswordQuestionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordAnswer", DbType="NVarChar(128)")]
		public string PasswordAnswer
		{
			get
			{
				return this._PasswordAnswer;
			}
			set
			{
				if ((this._PasswordAnswer != value))
				{
					this.OnPasswordAnswerChanging(value);
					this.SendPropertyChanging();
					this._PasswordAnswer = value;
					this.SendPropertyChanged("PasswordAnswer");
					this.OnPasswordAnswerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsApproved", DbType="Bit NOT NULL")]
		public bool IsApproved
		{
			get
			{
				return this._IsApproved;
			}
			set
			{
				if ((this._IsApproved != value))
				{
					this.OnIsApprovedChanging(value);
					this.SendPropertyChanging();
					this._IsApproved = value;
					this.SendPropertyChanged("IsApproved");
					this.OnIsApprovedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsLockedOut", DbType="Bit NOT NULL")]
		public bool IsLockedOut
		{
			get
			{
				return this._IsLockedOut;
			}
			set
			{
				if ((this._IsLockedOut != value))
				{
					this.OnIsLockedOutChanging(value);
					this.SendPropertyChanging();
					this._IsLockedOut = value;
					this.SendPropertyChanged("IsLockedOut");
					this.OnIsLockedOutChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._CreateDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastLoginDate", DbType="DateTime NOT NULL")]
		public System.DateTime LastLoginDate
		{
			get
			{
				return this._LastLoginDate;
			}
			set
			{
				if ((this._LastLoginDate != value))
				{
					this.OnLastLoginDateChanging(value);
					this.SendPropertyChanging();
					this._LastLoginDate = value;
					this.SendPropertyChanged("LastLoginDate");
					this.OnLastLoginDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastPasswordChangedDate", DbType="DateTime NOT NULL")]
		public System.DateTime LastPasswordChangedDate
		{
			get
			{
				return this._LastPasswordChangedDate;
			}
			set
			{
				if ((this._LastPasswordChangedDate != value))
				{
					this.OnLastPasswordChangedDateChanging(value);
					this.SendPropertyChanging();
					this._LastPasswordChangedDate = value;
					this.SendPropertyChanged("LastPasswordChangedDate");
					this.OnLastPasswordChangedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastLockoutDate", DbType="DateTime NOT NULL")]
		public System.DateTime LastLockoutDate
		{
			get
			{
				return this._LastLockoutDate;
			}
			set
			{
				if ((this._LastLockoutDate != value))
				{
					this.OnLastLockoutDateChanging(value);
					this.SendPropertyChanging();
					this._LastLockoutDate = value;
					this.SendPropertyChanged("LastLockoutDate");
					this.OnLastLockoutDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FailedPasswordAttemptCount", DbType="Int NOT NULL")]
		public int FailedPasswordAttemptCount
		{
			get
			{
				return this._FailedPasswordAttemptCount;
			}
			set
			{
				if ((this._FailedPasswordAttemptCount != value))
				{
					this.OnFailedPasswordAttemptCountChanging(value);
					this.SendPropertyChanging();
					this._FailedPasswordAttemptCount = value;
					this.SendPropertyChanged("FailedPasswordAttemptCount");
					this.OnFailedPasswordAttemptCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FailedPasswordAttemptWindowStart", DbType="DateTime NOT NULL")]
		public System.DateTime FailedPasswordAttemptWindowStart
		{
			get
			{
				return this._FailedPasswordAttemptWindowStart;
			}
			set
			{
				if ((this._FailedPasswordAttemptWindowStart != value))
				{
					this.OnFailedPasswordAttemptWindowStartChanging(value);
					this.SendPropertyChanging();
					this._FailedPasswordAttemptWindowStart = value;
					this.SendPropertyChanged("FailedPasswordAttemptWindowStart");
					this.OnFailedPasswordAttemptWindowStartChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FailedPasswordAnswerAttemptCount", DbType="Int NOT NULL")]
		public int FailedPasswordAnswerAttemptCount
		{
			get
			{
				return this._FailedPasswordAnswerAttemptCount;
			}
			set
			{
				if ((this._FailedPasswordAnswerAttemptCount != value))
				{
					this.OnFailedPasswordAnswerAttemptCountChanging(value);
					this.SendPropertyChanging();
					this._FailedPasswordAnswerAttemptCount = value;
					this.SendPropertyChanged("FailedPasswordAnswerAttemptCount");
					this.OnFailedPasswordAnswerAttemptCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FailedPasswordAnswerAttemptWindowsStart", DbType="DateTime NOT NULL")]
		public System.DateTime FailedPasswordAnswerAttemptWindowsStart
		{
			get
			{
				return this._FailedPasswordAnswerAttemptWindowsStart;
			}
			set
			{
				if ((this._FailedPasswordAnswerAttemptWindowsStart != value))
				{
					this.OnFailedPasswordAnswerAttemptWindowsStartChanging(value);
					this.SendPropertyChanging();
					this._FailedPasswordAnswerAttemptWindowsStart = value;
					this.SendPropertyChanged("FailedPasswordAnswerAttemptWindowsStart");
					this.OnFailedPasswordAnswerAttemptWindowsStartChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="NVarChar(256)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_Membership", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.Membership = null;
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.Membership = this;
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(System.Guid);
					}
					this.SendPropertyChanged("User");
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
