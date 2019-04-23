using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0,0]")]
	public partial class EnemyMovementNetworkObject : NetworkObject
	{
		public const int IDENTITY = 3;

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
		private float _maxHealth;
		public event FieldEvent<float> maxHealthChanged;
		public InterpolateFloat maxHealthInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float maxHealth
		{
			get { return _maxHealth; }
			set
			{
				// Don't do anything if the value is the same
				if (_maxHealth == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_maxHealth = value;
				hasDirtyFields = true;
			}
		}

		public void SetmaxHealthDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_maxHealth(ulong timestep)
		{
			if (maxHealthChanged != null) maxHealthChanged(_maxHealth, timestep);
			if (fieldAltered != null) fieldAltered("maxHealth", _maxHealth, timestep);
		}
		[ForgeGeneratedField]
		private float _currentHealth;
		public event FieldEvent<float> currentHealthChanged;
		public InterpolateFloat currentHealthInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float currentHealth
		{
			get { return _currentHealth; }
			set
			{
				// Don't do anything if the value is the same
				if (_currentHealth == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_currentHealth = value;
				hasDirtyFields = true;
			}
		}

		public void SetcurrentHealthDirty()
		{
			_dirtyFields[0] |= 0x4;
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
			maxHealthInterpolation.current = maxHealthInterpolation.target;
			currentHealthInterpolation.current = currentHealthInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _maxHealth);
			UnityObjectMapper.Instance.MapBytes(data, _currentHealth);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_maxHealth = UnityObjectMapper.Instance.Map<float>(payload);
			maxHealthInterpolation.current = _maxHealth;
			maxHealthInterpolation.target = _maxHealth;
			RunChange_maxHealth(timestep);
			_currentHealth = UnityObjectMapper.Instance.Map<float>(payload);
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
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _maxHealth);
			if ((0x4 & _dirtyFields[0]) != 0)
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
				if (maxHealthInterpolation.Enabled)
				{
					maxHealthInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					maxHealthInterpolation.Timestep = timestep;
				}
				else
				{
					_maxHealth = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_maxHealth(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (currentHealthInterpolation.Enabled)
				{
					currentHealthInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					currentHealthInterpolation.Timestep = timestep;
				}
				else
				{
					_currentHealth = UnityObjectMapper.Instance.Map<float>(data);
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
			if (maxHealthInterpolation.Enabled && !maxHealthInterpolation.current.UnityNear(maxHealthInterpolation.target, 0.0015f))
			{
				_maxHealth = (float)maxHealthInterpolation.Interpolate();
				//RunChange_maxHealth(maxHealthInterpolation.Timestep);
			}
			if (currentHealthInterpolation.Enabled && !currentHealthInterpolation.current.UnityNear(currentHealthInterpolation.target, 0.0015f))
			{
				_currentHealth = (float)currentHealthInterpolation.Interpolate();
				//RunChange_currentHealth(currentHealthInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public EnemyMovementNetworkObject() : base() { Initialize(); }
		public EnemyMovementNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public EnemyMovementNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
