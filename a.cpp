#include<iostream>
#include<sstream>
#include<string>
#include<map>
#include<fstream>
#include <pcap.h>
#pragma warning(disable:4996)
using namespace std;
map<string,string[3]> ftp;
bool flag;
u_char user[20];//�û���
u_char pass[20];//����


/* IPv4 header */
typedef struct ip_header
{
	u_char	ver_ihl;		// Version (4 bits) + Internet header length (4 bits)
	u_char	tos;			// Type of service 
	u_short tlen;			// Total length 
	u_short identification; // Identification
	u_short flags_fo;		// Flags (3 bits) + Fragment offset (13 bits)
	u_char	ttl;			// Time to live
	u_char	proto;			// Protocol
	u_short crc;			// Header checksum
	u_char  saddr[4];      // Դ��ַ(Source address)  
	u_char  daddr[4];      // Ŀ�ĵ�ַ(Destination address)  
	u_int	op_pad;			// Option + Padding
}ip_header;
typedef struct mac_header {
	u_char dest_addr[6];
	u_char src_addr[6];
	u_char type[2];
} mac_header;

/* TCP header*/
typedef struct tcp_header {
	u_short sport;            //Դ�˿ں�  
	u_short dport;             //Ŀ�Ķ˿ں�  
	u_int th_seq;                //���к�  
	u_int th_ack;               //ȷ�Ϻ�  
	u_int th1 : 4;              //tcpͷ������  
	u_int th_res : 4;             //6λ�е�4λ�ײ����� 
	u_int th_res2 : 2;            //6λ�е�2λ�ײ�����  
	u_char th_flags;            //6λ��־λ  
	u_short th_win;             //16λ���ڴ�С
	u_short th_sum;             //16λtcp�����  	
	u_short th_urp;             //16λ����ָ�� 
}tcp_header;



/* prototype of the packet handler */
void packet_handler(u_char *param, const struct pcap_pkthdr *header, const u_char *pkt_data);
string get_request_m_ip_message(const u_char* pkt_data);
string get_response_m_ip_message(const u_char* pkt_data);
void print(const struct pcap_pkthdr *header, string m_ip_message);


int main()
{
	flag = false;
	pcap_if_t *alldevs;
	pcap_if_t *d;
	int inum;
	int i = 0;
	pcap_t *adhandle;
	char errbuf[PCAP_ERRBUF_SIZE];
	u_int netmask;
	char packet_filter[] = "tcp";
	struct bpf_program fcode;

	/* Retrieve the device list */
	if (pcap_findalldevs(&alldevs, errbuf) == -1)
	{
		fprintf(stderr, "Error in pcap_findalldevs: %s\n", errbuf);
		exit(1);
	}


	/* Print the list */
	for (d = alldevs; d; d = d->next)
	{
		printf("%d. %s", ++i, d->name);
		if (d->description)
			printf(" (%s)\n", d->description);
		else
			printf(" (No description available)\n");
	}

	if (i == 0)
	{
		printf("\nNo interfaces found! Make sure WinPcap is installed.\n");
		return -1;
	}

	printf("Enter the interface number (1-%d):", i);
	scanf("%d", &inum);

	/* Check if the user specified a valid adapter */
	if (inum < 1 || inum > i)
	{
		printf("\nAdapter number out of range.\n");

		/* Free the device list */
		pcap_freealldevs(alldevs);
		return -1;
	}

	/* Jump to the selected adapter */
	for (d = alldevs, i = 0; i < inum - 1;d = d->next, i++);

	/* Open the adapter */
	if ((adhandle = pcap_open_live(d->name,	// name of the device
		65536,			// portion of the packet to capture. 
					   // 65536 grants that the whole packet will be captured on all the MACs.
        1,	// promiscuous mode (nonzero means promiscuous)
		1000,			// read timeout
		errbuf			// error buffer
	)) == NULL)
	{
		fprintf(stderr, "\nUnable to open the adapter. %s is not supported by WinPcap\n");
		/* Free the device list */
		pcap_freealldevs(alldevs);
		return -1;
	}

	/* Check the link layer. We support only Ethernet for simplicity. */
	if (pcap_datalink(adhandle) != DLT_EN10MB)
	{
		fprintf(stderr, "\nThis program works only on Ethernet networks.\n");
		/* Free the device list */
		pcap_freealldevs(alldevs);
		return -1;
	}

	if (d->addresses != NULL)
		/* Retrieve the mask of the first address of the interface */
		netmask = ((struct sockaddr_in *)(d->addresses->netmask))->sin_addr.S_un.S_addr;
	else
		/* If the interface is without addresses we suppose to be in a C class network */
		netmask = 0xffffff;


	//compile the filter
	if (pcap_compile(adhandle, &fcode, packet_filter, 1, netmask) < 0)
	{
		fprintf(stderr, "\nUnable to compile the packet filter. Check the syntax.\n");
		/* Free the device list */
		pcap_freealldevs(alldevs);
		return -1;
	}

	//set the filter
	if (pcap_setfilter(adhandle, &fcode) < 0)
	{
		fprintf(stderr, "\nError setting the filter.\n");
		/* Free the device list */
		pcap_freealldevs(alldevs);
		return -1;
	}

	printf("\nlistening on %s...\n", d->description);

	/* At this point, we don't need any more the device list. Free it */
	pcap_freealldevs(alldevs);

	/* start the capture */
	pcap_loop(adhandle, 0, packet_handler, NULL);
	return 0;
}

/* Callback function invoked by libpcap for every incoming packet */
	string get_request_m_ip_message(const u_char* pkt_data)
	{
		mac_header *mh;

		ip_header *ih;

		string m_ip_message;

		string str;//empty string

		ostringstream sout;
		int length = sizeof(mac_header) + sizeof(ip_header);

		mh = (mac_header*)pkt_data;

		ih = (ip_header*)(pkt_data + sizeof(mac_header));
		for (int i = 0; i < 5; i++)

			sout << hex << (int)(mh->src_addr[i]) << "-";

		sout << (int)(mh->src_addr[5]) << ",";
		for (int i = 0; i < 3; i++)

			sout << dec << (int)(ih->saddr[i]) << ".";

		sout << (int)(ih->saddr[3]) << ",";
		for (int i = 0; i < 5; i++)

			sout << hex << (int)(mh->dest_addr[i]) << "-";

		sout << (int)(mh->dest_addr[5]) << ",";
		for (int i = 0; i < 3; i++)

			sout << dec << (int)(ih->daddr[i]) << ".";

		sout << (int)(ih->daddr[3]);
		m_ip_message = sout.str();

		return m_ip_message;
	}
	string get_response_m_ip_message(const u_char* pkt_data)
	{
		mac_header *mh;

		ip_header *ih;

		string m_ip_message;

		string str;//empty string

		ostringstream sout;
		int length = sizeof(mac_header) + sizeof(ip_header);	
		mh = (mac_header*)pkt_data;	ih = (ip_header*)(pkt_data + sizeof(mac_header));	
		for (int i = 0; i < 5; i++)		
			sout << hex << (int)(mh->dest_addr[i]) << "-";	
		sout << (int)(mh->dest_addr[5]) << ",";
		for (int i = 0; i < 3; i++)

			sout << dec << (int)(ih->daddr[i]) << ".";

		sout << (int)(ih->daddr[3]) << ",";
		for (int i = 0; i < 5; i++)

			sout << hex << (int)(mh->src_addr[i]) << "-";

		sout << (int)(mh->src_addr[5]) << ",";
		for (int i = 0; i < 3; i++)

			sout << dec << (int)(ih->saddr[i]) << ".";

		sout << (int)(ih->saddr[3]);
		m_ip_message = sout.str();

		return m_ip_message;
	}
	void output(ip_header* ih, mac_header* mh, const struct pcap_pkthdr* header, char user[], char pass[], bool isSucceed)
	{
		if (user[0] == '\0')
			return;

		char timestr[46];
		struct tm* ltime;
		time_t local_tv_sec;

		/*��ʱ���ת���ɿ�ʶ��ĸ�ʽ */
		local_tv_sec = header->ts.tv_sec;
		ltime = localtime(&local_tv_sec);
		strftime(timestr, sizeof timestr, "%Y-%m-%d %H:%M:%S", ltime);

		printf("%s,", timestr);

		printf("%02X-%02X-%02X-%02X-%02X-%02X,",
			mh->dest_addr[0],
			mh->dest_addr[1],
			mh->dest_addr[2],
			mh->dest_addr[3],
			mh->dest_addr[4],
			mh->dest_addr[5]);//�ͻ�����ַ
		printf("%d.%d.%d.%d,",
			ih->daddr[0],
			ih->daddr[1],
			ih->daddr[2],
			ih->daddr[3]);//�ͻ���IP

		printf("%02X-%02X-%02X-%02X-%02X-%02X,",
			mh->src_addr[0],
			mh->src_addr[1],
			mh->src_addr[2],
			mh->src_addr[3],
			mh->src_addr[4],
			mh->src_addr[5]);//FTP��������ַ
		printf("%d.%d.%d.%d,",
			ih->saddr[0],
			ih->saddr[1],
			ih->saddr[2],
			ih->saddr[3]);//FTP������IP

		printf("%s,%s,", user, pass);//�û�������

		if (isSucceed) {
			printf("SUCCEED\n");
		}
		else {
			printf("FAILED\n");
		}


	
		FILE* fp = fopen("output.csv", "a+");
		fprintf(fp, "%s,", timestr);//ʱ��

		fprintf(fp, "%02X-%02X-%02X-%02X-%02X-%02X,",
			mh->dest_addr[0],
			mh->dest_addr[1],
			mh->dest_addr[2],
			mh->dest_addr[3],
			mh->dest_addr[4],
			mh->dest_addr[5]);//�ͻ�����ַ
		fprintf(fp, "%d.%d.%d.%d,",
			ih->daddr[0],
			ih->daddr[1],
			ih->daddr[2],
			ih->daddr[3]);//�ͻ���IP

		fprintf(fp, "%02X-%02X-%02X-%02X-%02X-%02X,",
			mh->src_addr[0],
			mh->src_addr[1],
			mh->src_addr[2],
			mh->src_addr[3],
			mh->src_addr[4],
			mh->src_addr[5]);//FTP��������ַ
		fprintf(fp, "%d.%d.%d.%d,",
			ih->saddr[0],
			ih->saddr[1],
			ih->saddr[2],
			ih->saddr[3]);//FTP������IP

		fprintf(fp, "%s,%s,", user, pass);//�û�������

		if (isSucceed) {
			fprintf(fp, "SUCCEED\n");
		}
		else {
			fprintf(fp, "FAILED\n");
		}
		fclose(fp);

		user[0] = '\0';
	}
	void packet_handler(u_char* param, const struct pcap_pkthdr* header, const u_char* pkt_data)
	{
		ip_header* ih;
		mac_header* mh;
		u_int i = 0;

		int length = sizeof(mac_header) + sizeof(ip_header);
		mh = (mac_header*)pkt_data;
		ih = (ip_header*)(pkt_data + 14); //length of ethernet header

		int name_point = 0;
		int pass_point = 0;
		int tmp;
		for (int i = 0; i < ih->tlen - 40; i++) {
			if (*(pkt_data + i) == 'U' && *(pkt_data + i + 1) == 'S' && *(pkt_data + i + 2) == 'E' && *(pkt_data + i + 3) == 'R') {
				name_point = i + 5;//'u' 's' 'e' 'r' ' '��5���ֽ�,��ת���û�����һ���ֽ�

				//���س�������Ϊֹ��ǰ����������û���
				int j = 0;
				while (!(*(pkt_data + name_point) == 13 && *(pkt_data + name_point + 1) == 10)) {
					user[j] = *(pkt_data + name_point);//�洢�û���
					j++;
					++name_point;
				}
				user[j] = '\0';
				break;

			}

			if (*(pkt_data + i) == 'P' && *(pkt_data + i + 1) == 'A' && *(pkt_data + i + 2) == 'S' && *(pkt_data + i + 3) == 'S') {
				pass_point = i + 5;////'P' 'A' 'S' 'S' ' '��5���ֽ�,��ת�������һ���ֽ�
				tmp = pass_point;

				//���س�������Ϊֹ��ǰ�������������
				int k = 0;
				while (!(*(pkt_data + pass_point) == 13 && *(pkt_data + pass_point + 1) == 10)) {
					pass[k] = *(pkt_data + pass_point);//�洢����
					k++;
					++pass_point;

				}
				pass[k] = '\0';

				for (;; tmp++) {
					if (*(pkt_data + tmp) == '2' && *(pkt_data + tmp + 1) == '3' && *(pkt_data + tmp + 2) == '0') {
						output(ih, mh, header, (char*)user, (char*)pass, true);
						break;
					}
					else if (*(pkt_data + tmp) == '5' && *(pkt_data + tmp + 1) == '3' && *(pkt_data + tmp + 2) == '0') {
						output(ih, mh, header, (char*)user, (char*)pass, false);
						break;
					}
				}
				break;
			}
		}
	}

	