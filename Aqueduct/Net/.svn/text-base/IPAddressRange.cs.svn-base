using System;
using System.Net;
using System.Net.Sockets;

namespace Aqueduct.Net
{
    public class IPAddressRange
    {
        private AddressFamily m_addressFamily;
        private Byte[] m_lowerBytes;
        private Byte[] m_upperBytes;

        public IPAddressRange(IPAddress lower, IPAddress upper)
        {
            // Assert that lower.AddressFamily == upper.AddressFamily
            m_addressFamily = lower.AddressFamily;
            m_lowerBytes = lower.GetAddressBytes();
            m_upperBytes = upper.GetAddressBytes();
        }

        public bool IsInRange(IPAddress address)
        {
            if (address.AddressFamily != m_addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < this.m_lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < m_lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > m_upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == m_lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == m_upperBytes[i]);
            }

            return true;
        }

    }
}
