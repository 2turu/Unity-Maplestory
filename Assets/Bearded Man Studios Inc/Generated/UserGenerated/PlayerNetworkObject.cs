using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0,0,0,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
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
		private float _Speed;
		public event FieldEvent<float> SpeedChanged;
		public InterpolateFloat SpeedInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float Speed
		{
			get { return _Speed; }
			set
			{
				// Don't do anything if the value is the same
				if (_Speed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_Speed = value;
				hasDirtyFields = true;
			}
		}

		public void SetSpeedDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_Speed(ulong timestep)
		{
			if (SpeedChanged != null) SpeedChanged(_Speed, timestep);
			if (fieldAltered != null) fieldAltered("Speed", _Speed, timestep);
		}
		[ForgeGeneratedField]
		private bool _isJumping;
		public event FieldEvent<bool> isJumpingChanged;
		public Interpolated<bool> isJumpingInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isJumping
		{
			get { return _isJumping; }
			set
			{
				// Don't do anything if the value is the same
				if (_isJumping == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_isJumping = value;
				hasDirtyFields = true;
			}
		}

		public void SetisJumpingDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_isJumping(ulong timestep)
		{
			if (isJumpingChanged != null) isJumpingChanged(_isJumping, timestep);
			if (fieldAltered != null) fieldAltered("isJumping", _isJumping, timestep);
		}
		[ForgeGeneratedField]
		private bool _isCrouching;
		public event FieldEvent<bool> isCrouchingChanged;
		public Interpolated<bool> isCrouchingInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isCrouching
		{
			get { return _isCrouching; }
			set
			{
				// Don't do anything if the value is the same
				if (_isCrouching == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_isCrouching = value;
				hasDirtyFields = true;
			}
		}

		public void SetisCrouchingDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_isCrouching(ulong timestep)
		{
			if (isCrouchingChanged != null) isCrouchingChanged(_isCrouching, timestep);
			if (fieldAltered != null) fieldAltered("isCrouching", _isCrouching, timestep);
		}
		[ForgeGeneratedField]
		private bool _isGrounded;
		public event FieldEvent<bool> isGroundedChanged;
		public Interpolated<bool> isGroundedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isGrounded
		{
			get { return _isGrounded; }
			set
			{
				// Don't do anything if the value is the same
				if (_isGrounded == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_isGrounded = value;
				hasDirtyFields = true;
			}
		}

		public void SetisGroundedDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_isGrounded(ulong timestep)
		{
			if (isGroundedChanged != null) isGroundedChanged(_isGrounded, timestep);
			if (fieldAltered != null) fieldAltered("isGrounded", _isGrounded, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			SpeedInterpolation.current = SpeedInterpolation.target;
			isJumpingInterpolation.current = isJumpingInterpolation.target;
			isCrouchingInterpolation.current = isCrouchingInterpolation.target;
			isGroundedInterpolation.current = isGroundedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _Speed);
			UnityObjectMapper.Instance.MapBytes(data, _isJumping);
			UnityObjectMapper.Instance.MapBytes(data, _isCrouching);
			UnityObjectMapper.Instance.MapBytes(data, _isGrounded);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_Speed = UnityObjectMapper.Instance.Map<float>(payload);
			SpeedInterpolation.current = _Speed;
			SpeedInterpolation.target = _Speed;
			RunChange_Speed(timestep);
			_isJumping = UnityObjectMapper.Instance.Map<bool>(payload);
			isJumpingInterpolation.current = _isJumping;
			isJumpingInterpolation.target = _isJumping;
			RunChange_isJumping(timestep);
			_isCrouching = UnityObjectMapper.Instance.Map<bool>(payload);
			isCrouchingInterpolation.current = _isCrouching;
			isCrouchingInterpolation.target = _isCrouching;
			RunChange_isCrouching(timestep);
			_isGrounded = UnityObjectMapper.Instance.Map<bool>(payload);
			isGroundedInterpolation.current = _isGrounded;
			isGroundedInterpolation.target = _isGrounded;
			RunChange_isGrounded(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Speed);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isJumping);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isCrouching);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isGrounded);

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
				if (SpeedInterpolation.Enabled)
				{
					SpeedInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					SpeedInterpolation.Timestep = timestep;
				}
				else
				{
					_Speed = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_Speed(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (isJumpingInterpolation.Enabled)
				{
					isJumpingInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isJumpingInterpolation.Timestep = timestep;
				}
				else
				{
					_isJumping = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isJumping(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (isCrouchingInterpolation.Enabled)
				{
					isCrouchingInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isCrouchingInterpolation.Timestep = timestep;
				}
				else
				{
					_isCrouching = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isCrouching(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (isGroundedInterpolation.Enabled)
				{
					isGroundedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isGroundedInterpolation.Timestep = timestep;
				}
				else
				{
					_isGrounded = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isGrounded(timestep);
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
			if (SpeedInterpolation.Enabled && !SpeedInterpolation.current.UnityNear(SpeedInterpolation.target, 0.0015f))
			{
				_Speed = (float)SpeedInterpolation.Interpolate();
				//RunChange_Speed(SpeedInterpolation.Timestep);
			}
			if (isJumpingInterpolation.Enabled && !isJumpingInterpolation.current.UnityNear(isJumpingInterpolation.target, 0.0015f))
			{
				_isJumping = (bool)isJumpingInterpolation.Interpolate();
				//RunChange_isJumping(isJumpingInterpolation.Timestep);
			}
			if (isCrouchingInterpolation.Enabled && !isCrouchingInterpolation.current.UnityNear(isCrouchingInterpolation.target, 0.0015f))
			{
				_isCrouching = (bool)isCrouchingInterpolation.Interpolate();
				//RunChange_isCrouching(isCrouchingInterpolation.Timestep);
			}
			if (isGroundedInterpolation.Enabled && !isGroundedInterpolation.current.UnityNear(isGroundedInterpolation.target, 0.0015f))
			{
				_isGrounded = (bool)isGroundedInterpolation.Interpolate();
				//RunChange_isGrounded(isGroundedInterpolation.Timestep);
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
