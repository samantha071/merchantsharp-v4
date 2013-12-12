﻿using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents {
	class UIComboBox {

		private static VendorManagerImpl vendorManagerImpl = new VendorManagerImpl();
		private static SellingPriceManagerImpl sellingPriceManagerImpl = new SellingPriceManagerImpl();
		private static StockManagerImpl stockManagerImpl = new StockManagerImpl();
		private static CategoryManagerImpl categoryManagerImpl = new CategoryManagerImpl();
		private static CompanyManagerImpl companyManagerImpl = new CompanyManagerImpl();
		private static ItemManagerImpl itemManagerImpl = new ItemManagerImpl();
		private static BankManagerImpl bankManagerImpl = new BankManagerImpl();
		private static UserManagerImpl userManagerImpl = new UserManagerImpl();
		public static DataTable vendorDataTable = null;
		public static DataTable vendorDataTableFilter = null;
		public static AddVendor addVendor = null;
		public static DataTable stockLocationDataTable = null;
		public static DataTable categoryDataTable = null;
		public static DataTable companyDataTable = null;
		public static DataTable bankDataTable = null;
		public static AddBank addBank = null;
		public static DataTable userDataTable = null;
		public static DataTable buyingInvoiceStatusDataTable = null;
		public static DataTable yesNoDataTable = null;

		public static void vendorsForAddBuyingInvoice(MSComboBox comboBox) {
			try {
				if(vendorDataTable == null) {
					vendorDataTable = new DataTable();
					vendorDataTable.Columns.Add("ID", typeof(int));
					vendorDataTable.Columns.Add("name", typeof(String));
				} else {
					vendorDataTable.Rows.Clear();
				}
				List<Vendor> list = vendorManagerImpl.getAllActivedVendors();
				foreach(Vendor vendor in list) {
					vendorDataTable.Rows.Add(vendor.Id, vendor.Name);
				}
				if(Session.Permission["canAddVendor"] == 1 && comboBox.AddLinkWindow == null) {
					if(addVendor == null) {
						addVendor = new AddVendor(comboBox);
					}
					comboBox.AddLinkWindow = addVendor;
				}
				comboBox.IsPermissionDenied = Session.Permission["canAddVendor"] == 0 ? true : false;
				comboBox.OptionGroup = vendorDataTable;
			} catch(Exception) {
			}
		}

		public static void vendorsForFilter(MSComboBox comboBox) {
			try {
				if(vendorDataTableFilter == null) {
					vendorDataTableFilter = new DataTable();
					vendorDataTableFilter.Columns.Add("ID", typeof(int));
					vendorDataTableFilter.Columns.Add("name", typeof(String));
				} else {
					vendorDataTableFilter.Rows.Clear();
				}
				List<Vendor> list = vendorManagerImpl.getAllActivedVendors();
				vendorDataTableFilter.Rows.Add(-1, "All");
				foreach(Vendor vendor in list) {
					vendorDataTableFilter.Rows.Add(vendor.Id, vendor.Name);
				}
				comboBox.IsPermissionDenied = Session.Permission["canAddVendor"] == 0 ? true : false;
				comboBox.OptionGroup = vendorDataTableFilter;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		public static void sellingPriceForItemAndMode(MSComboBox comboBox, int itemId, String mode, AddSellingPrice addSellingPrice) {
			try {
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ID", typeof(int));
				dataTable.Columns.Add("price", typeof(String));
				List<SellingPrice> list = sellingPriceManagerImpl.getSellingPriceByItemAndMode(itemId, mode);
				foreach(SellingPrice sellingPrice in list) {
					dataTable.Rows.Add(sellingPrice.Id, sellingPrice.Price.ToString("#,##0.00"));
				}
				if(Session.Permission["canAddSellingPrice"] == 1) {
					comboBox.AddLinkWindow = addSellingPrice;
					addSellingPrice.Mode = mode;
					addSellingPrice.ItemId = itemId;
					addSellingPrice.ComboBox = comboBox;
				} else {
					comboBox.AddLinkWindow = null;
				}
				int index = dataTable.Rows.Count - 1;
				comboBox.OptionGroup = dataTable;
				if(index > -1) {
					comboBox.SelectedIndex = index;
				}
			} catch(Exception) {
			}
		}

		public static void loadStocks(MSComboBox comboBox) {
			try {
				if(stockLocationDataTable == null) {
					stockLocationDataTable = new DataTable();
					stockLocationDataTable.Columns.Add("ID", typeof(int));
					stockLocationDataTable.Columns.Add("name", typeof(String));
					List<StockLocation> list = stockManagerImpl.getStockLocations();
					foreach(StockLocation stockLocation in list) {
						stockLocationDataTable.Rows.Add(stockLocation.Id, stockLocation.Name);
					}
				}
				comboBox.OptionGroup = stockLocationDataTable;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		public static void categoriesForSelect(MSComboBox comboBox) {
			try {
				if(categoryDataTable == null) {
					categoryDataTable = new DataTable();
					categoryDataTable.Columns.Add("ID", typeof(int));
					categoryDataTable.Columns.Add("name", typeof(String));
					categoryDataTable.Rows.Add(0, "- Select -");
					List<Category> cats = categoryManagerImpl.getAllCategories();
					foreach(Category category in cats) {
						categoryDataTable.Rows.Add(category.Id, category.Name);
					}
				}
				comboBox.OptionGroup = categoryDataTable;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		public static void companiesForSelect(MSComboBox comboBox) {
			try {
				if(companyDataTable == null) {
					companyDataTable = new DataTable();
					companyDataTable.Columns.Add("ID", typeof(int));
					companyDataTable.Columns.Add("name", typeof(String));
					companyDataTable.Rows.Add(0, "- Select -");
					List<Company> coms = companyManagerImpl.getAllCompanies();
					foreach(Company company in coms) {
						companyDataTable.Rows.Add(company.Id, company.Name);
					}
				}
				comboBox.OptionGroup = companyDataTable;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		internal static void companiesForCategory(MSComboBox comboBox_company_selectItem, int categoryId) {
			try {
				if(categoryId == 0) {
					companiesForSelect(comboBox_company_selectItem);
				} else {
					List<int> companyIds = itemManagerImpl.getCompanyIdsForCategory(categoryId);
					DataTable data = new DataTable();
					data.Columns.Add("ID", typeof(int));
					data.Columns.Add("name", typeof(String));
					data.Rows.Add(0, "- Select -");
					foreach(int id in companyIds) {
						data.Rows.Add(id, companyManagerImpl.getCompanyNameById(id));
					}
					comboBox_company_selectItem.OptionGroup = data;
					comboBox_company_selectItem.SelectedIndex = 0;
				}
			} catch(Exception) {
			}
		}

		internal static void itemsForCategoryAndCompany(MSComboBox comboBox_item_selectItem, int categoryId, int companyId) {
			try {
				DataTable data = new DataTable();
				data.Columns.Add("ID", typeof(int));
				data.Columns.Add("name", typeof(String));
				data.Rows.Add(0, "- Select -");
				if(categoryId > 0 || companyId > 0) {
					if(categoryId < 1) {
						categoryId = -1;
					}
					if(companyId < 1) {
						companyId = -1;
					}
					List<Item> list = itemManagerImpl.getItemsForCategoryAndCompany(categoryId, companyId);
					foreach(Item item in list) {
						data.Rows.Add(item.Id, item.Name);
					}
				}
				comboBox_item_selectItem.OptionGroup = data;
				comboBox_item_selectItem.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		public static void banksForSelect(MSComboBox comboBox) {
			try {
				if(bankDataTable == null) {
					bankDataTable = new DataTable();
					bankDataTable.Columns.Add("ID", typeof(int));
					bankDataTable.Columns.Add("name", typeof(String));
				} else {
					bankDataTable.Rows.Clear();
				}
				List<Bank> list = bankManagerImpl.getAllBanks();
				foreach(Bank bank in list) {
					bankDataTable.Rows.Add(bank.Id, bank.Name);
				}
				if(Session.Permission["canAddBank"] == 1 && comboBox.AddLinkWindow == null) {
					if(addBank == null) {
						addBank = new AddBank(comboBox);
					}
					comboBox.AddLinkWindow = addBank;
				}
				comboBox.IsPermissionDenied = Session.Permission["canAddBank"] == 0 ? true : false;
				comboBox.OptionGroup = bankDataTable;
			} catch(Exception) {
			}
		}

		public static void usersForFilter(MSComboBox comboBox) {
			try {
				if(userDataTable == null) {
					userDataTable = new DataTable();
					userDataTable.Columns.Add("ID", typeof(int));
					userDataTable.Columns.Add("name", typeof(String));
				} else {
					userDataTable.Rows.Clear();
				}
				List<User> list = userManagerImpl.getAllUsers();
				userDataTable.Rows.Add(-1, "All");
				foreach(User user in list) {
					if(user.IsFake == 0) {
						userDataTable.Rows.Add(user.Id, user.FirstName + " " + user.LastName);
					}
				}
				comboBox.OptionGroup = userDataTable;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		public static void buyingInvoiceStatusForSelect(MSComboBox comboBox) {
			try {
				if(buyingInvoiceStatusDataTable == null) {
					buyingInvoiceStatusDataTable = new DataTable();
					buyingInvoiceStatusDataTable.Columns.Add("ID", typeof(int));
					buyingInvoiceStatusDataTable.Columns.Add("name", typeof(String));

					buyingInvoiceStatusDataTable.Rows.Add(-1, "All");
					buyingInvoiceStatusDataTable.Rows.Add(1, "Received");
					buyingInvoiceStatusDataTable.Rows.Add(2, "Request");
					buyingInvoiceStatusDataTable.Rows.Add(3, "Draft");
				}
				comboBox.OptionGroup = buyingInvoiceStatusDataTable;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		public static void yesNoForSelect(MSComboBox comboBox) {
			try {
				if(yesNoDataTable == null) {
					yesNoDataTable = new DataTable();
					yesNoDataTable.Columns.Add("ID", typeof(int));
					yesNoDataTable.Columns.Add("name", typeof(String));

					yesNoDataTable.Rows.Add(-1, "All");
					yesNoDataTable.Rows.Add(0, "No");
					yesNoDataTable.Rows.Add(1, "Yes");
				}
				comboBox.OptionGroup = yesNoDataTable;
				comboBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}
	}
}
