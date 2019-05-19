using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.25,0,0,0,0,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 3;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.25f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private float _horizontal;
		public event FieldEvent<float> horizontalChanged;
		public InterpolateFloat horizontalInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float horizontal
		{
			get { return _horizontal; }
			set
			{
				// Don't do anything if the value is the same
				if (_horizontal == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_horizontal = value;
				hasDirtyFields = true;
			}
		}

		public void SethorizontalDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_horizontal(ulong timestep)
		{
			if (horizontalChanged != null) horizontalChanged(_horizontal, timestep);
			if (fieldAltered != null) fieldAltered("horizontal", _horizontal, timestep);
		}
		[ForgeGeneratedField]
		private bool _jump;
		public event FieldEvent<bool> jumpChanged;
		public Interpolated<bool> jumpInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool jump
		{
			get { return _jump; }
			set
			{
				// Don't do anything if the value is the same
				if (_jump == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_jump = value;
				hasDirtyFields = true;
			}
		}

		public void SetjumpDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_jump(ulong timestep)
		{
			if (jumpChanged != null) jumpChanged(_jump, timestep);
			if (fieldAltered != null) fieldAltered("jump", _jump, timestep);
		}
		[ForgeGeneratedField]
		private bool _crouch;
		public event FieldEvent<bool> crouchChanged;
		public Interpolated<bool> crouchInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool crouch
		{
			get { return _crouch; }
			set
			{
				// Don't do anything if the value is the same
				if (_crouch == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_crouch = value;
				hasDirtyFields = true;
			}
		}

		public void SetcrouchDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_crouch(ulong timestep)
		{
			if (crouchChanged != null) crouchChanged(_crouch, timestep);
			if (fieldAltered != null) fieldAltered("crouch", _crouch, timestep);
		}
		[ForgeGeneratedField]
		private bool _fire1;
		public event FieldEvent<bool> fire1Changed;
		public Interpolated<bool> fire1Interpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool fire1
		{
			get { return _fire1; }
			set
			{
				// Don't do anything if the value is the same
				if (_fire1 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_fire1 = value;
				hasDirtyFields = true;
			}
		}

		public void Setfire1Dirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_fire1(ulong timestep)
		{
			if (fire1Changed != null) fire1Changed(_fire1, timestep);
			if (fieldAltered != null) fieldAltered("fire1", _fire1, timestep);
		}
		[ForgeGeneratedField]
		private int _currentHealth;
		public event FieldEvent<int> currentHealthChanged;
		public Interpolated<int> currentHealthInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int currentHealth
		{
			get { return _currentHealth; }
			set
			{
				// Don't do anything if the value is the same
				if (_currentHealth == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_currentHealth = value;
				hasDirtyFields = true;
			}
		}

		public void SetcurrentHealthDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_currentHealth(ulong timestep)
		{
			if (currentHealthChanged != null) currentHealthChanged(_currentHealth, timestep);
			if (fieldAltered != null) fieldAltered("currentHealth", _currentHealth, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			horizontalInterpolation.current = horizontalInterpolation.target;
			jumpInterpolation.current = jumpInterpolation.target;
			crouchInterpolation.current = crouchInterpolation.target;
			fire1Interpolation.current = fire1Interpolation.target;
			currentHealthInterpolation.current = currentHealthInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _horizontal);
			UnityObjectMapper.Instance.MapBytes(data, _jump);
			UnityObjectMapper.Instance.MapBytes(data, _crouch);
			UnityObjectMapper.Instance.MapBytes(data, _fire1);
			UnityObjectMapper.Instance.MapBytes(data, _currentHealth);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_horizontal = UnityObjectMapper.Instance.Map<float>(payload);
			horizontalInterpolation.current = _horizontal;
			horizontalInterpolation.target = _horizontal;
			RunChange_horizontal(timestep);
			_jump = UnityObjectMapper.Instance.Map<bool>(payload);
			jumpInterpolation.current = _jump;
			jumpInterpolation.target = _jump;
			RunChange_jump(timestep);
			_crouch = UnityObjectMapper.Instance.Map<bool>(payload);
			crouchInterpolation.current = _crouch;
			crouchInterpolation.target = _crouch;
			RunChange_crouch(timestep);
			_fire1 = UnityObjectMapper.Instance.Map<bool>(payload);
			fire1Interpolation.current = _fire1;
			fire1Interpolation.target = _fire1;
			RunChange_fire1(timestep);
			_currentHealth = UnityObjectMapper.Instance.Map<int>(payload);
			currentHealthInterpolation.current = _currentHealth;
			currentHealthInterpolation.target = _currentHealth;
			RunChange_currentHealth(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _horizontal);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _jump);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _crouch);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _fire1);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _currentHealth);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (horizontalInterpolation.Enabled)
				{
					horizontalInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					horizontalInterpolation.Timestep = timestep;
				}
				else
				{
					_horizontal = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_horizontal(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (jumpInterpolation.Enabled)
				{
					jumpInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					jumpInterpolation.Timestep = timestep;
				}
				else
				{
					_jump = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_jump(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (crouchInterpolation.Enabled)
				{
					crouchInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					crouchInterpolation.Timestep = timestep;
				}
				else
				{
					_crouch = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_crouch(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (fire1Interpolation.Enabled)
				{
					fire1Interpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					fire1Interpolation.Timestep = timestep;
				}
				else
				{
					_fire1 = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_fire1(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (currentHealthInterpolation.Enabled)
				{
					currentHealthInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					currentHealthInterpolation.Timestep = timestep;
				}
				else
				{
					_currentHealth = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_currentHealth(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (horizontalInterpolation.Enabled && !horizontalInterpolation.current.UnityNear(horizontalInterpolation.target, 0.0015f))
			{
				_horizontal = (float)horizontalInterpolation.Interpolate();
				//RunChange_horizontal(horizontalInterpolation.Timestep);
			}
			if (jumpInterpolation.Enabled && !jumpInterpolation.current.UnityNear(jumpInterpolation.target, 0.0015f))
			{
				_jump = (bool)jumpInterpolation.Interpolate();
				//RunChange_jump(jumpInterpolation.Timestep);
			}
			if (crouchInterpolation.Enabled && !crouchInterpolation.current.UnityNear(crouchInterpolation.target, 0.0015f))
			{
				_crouch = (bool)crouchInterpolation.Interpolate();
				//RunChange_crouch(crouchInterpolation.Timestep);
			}
			if (fire1Interpolation.Enabled && !fire1Interpolation.current.UnityNear(fire1Interpolation.target, 0.0015f))
			{
				_fire1 = (bool)fire1Interpolation.Interpolate();
				//RunChange_fire1(fire1Interpolation.Timestep);
			}
			if (currentHealthInterpolation.Enabled && !currentHealthInterpolation.current.UnityNear(currentHealthInterpolation.target, 0.0015f))
			{
				_currentHealth = (int)currentHealthInterpolation.Interpolate();
				//RunChange_currentHealth(currentHealthInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
