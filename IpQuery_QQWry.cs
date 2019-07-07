using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IpQuery_QQWry {
	public class IpQuery_QQWry {
		public IpQuery_QQWry (string _file) {
			m_bytes = File.ReadAllBytes (_file);
			m_index_head = read_item<uint> (0);
			m_index_tail = read_item<uint> (4);
		}

		public (string, string) find_info (string _ip) {
			var _ipitem = _ip.Split ('.');
			uint _nip = (Convert.ToUInt32 (_ipitem[0]) << 24) | (Convert.ToUInt32 (_ipitem[1]) << 16);
			_nip |= (Convert.ToUInt32 (_ipitem[2]) << 8) | Convert.ToUInt32 (_ipitem[3]);
			var _head = m_index_head;
			var _tail = m_index_tail;
			while (_tail > _head) {
				uint _cur = ((_tail - _head) / 7) >> 1;
				_cur = _head + (_cur == 0 ? 1 : _cur) * 7;
				uint _ip_min = read_item<uint> (_cur);
				uint _pos = read_item<uint> (_cur + 4) & 0xffffff;
				uint _ip_max = read_item<uint> (_pos);
				if (_nip < _ip_min) {
					_tail = _cur;
				} else if (_nip > _ip_max) {
					_head = _cur;
				} else {
					_pos += 4;
					byte _mode = m_bytes [_pos];
					if (_mode == 0x01) {
						if (m_bytes [read_item<uint> (_pos + 1) & 0xffffff] == 0x02) {
							uint _main_offset = read_item<uint> (_pos + 1) & 0xffffff;
							return read_infos (_main_offset, _pos + 8);
						} else {
							_pos = read_item<uint> (_pos + 1) & 0xffffff;
							return read_infos (_pos);
						}
					} else if (_mode == 0x02) {
						uint _main_offset = read_item<uint> (_pos + 1) & 0xffffff;
						return read_infos (_main_offset, _pos + 4);
					} else {
						return read_infos (_pos);
					}
				}
			}
			return ("", "");
		}

		private (string, string) read_infos (uint _main_pos, uint _sub_pos = 0) {
			string _main_info = read_item<string> (_main_pos);
			_sub_pos = (_sub_pos == 0 ? (_main_pos + (uint) Encoding.GetEncoding ("GB2312").GetByteCount (_main_info)) : _sub_pos);
			string _sub_info = read_item<string> (_sub_pos);
			return (_main_info, _sub_info);
		}

		private T read_item<T> (uint _pos) {
			if (typeof (T) == typeof (uint)) {
				uint _n = (uint) m_bytes [_pos];
				_n += ((uint) m_bytes [_pos + 1]) << 8;
				_n += ((uint) m_bytes [_pos + 2]) << 16;
				_n += ((uint) m_bytes [_pos + 3]) << 24;
				return (T) (object) _n;
			} else if (typeof (T) == typeof (string)) {
				List<byte> _l = new List<byte> ();
				if (m_bytes [_pos] == 0x01 || m_bytes [_pos] == 0x02) {
					_pos = read_item<uint> (_pos + 1) & 0xffffff;
					if (m_bytes [_pos] >= 0x00 && m_bytes [_pos] <= 0x02)
						return (T) (object) "";
				}
				while (m_bytes [_pos] != 0x00) {
					_l.Add (m_bytes [_pos++]);
				}
				return (T) (object) Encoding.GetEncoding ("GB2312").GetString (_l.ToArray ());
			}
			throw new Exception ("暂不支持T类型");
		}

		private byte [] m_bytes = null;
		private uint m_index_head = 0;
		private uint m_index_tail = 0;
	}
}
